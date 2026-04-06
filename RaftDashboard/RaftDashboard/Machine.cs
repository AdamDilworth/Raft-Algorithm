using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RaftDashboard
{
    public class Machine
    {
        /* Data Members */

        // Identification
        private readonly int ID;

        // Randomness
        private static readonly Random rng = new();
        private readonly int minDelay = 150;
        private readonly int maxDelay = 300;
        private readonly double lossChance = 0.05;

        // RAFT Leadership
        public enum Roles { Follower, Candidate, Leader }
        public Roles Role { get; private set; }
        private int CurrentTerm = 0; // This should increment after each election
        private int CommitIndex = 0; // Incremented after every commit
        private int LastApplied = 0; // 
        private int responseCount = 0; // Used for consensus
        private double lastHeartbeatTime = 0;
        private double electionTimeout;
        private int? VotedFor = null;
        private int votesReceived = 0;

        // Data for multithreaded and asynchronous behavior
        private CancellationTokenSource cts;
        private ManualResetEventSlim pauseEvent;

        // Message Passing Bus
        public Channel<string> Inbox { get; } = Channel.CreateUnbounded<string>();
        private readonly List<Machine> _peers;
        private string MessageDisplay = "";

        // Log
        public IReadOnlyList<LogEntry> Log => _log.AsReadOnly();
        private readonly List<LogEntry> _log = [];

        // Stopwatch and Event
        public double Time { get; private set; }

        /* Methods */

        // Constructor
        public Machine(int id, List<Machine> peers)
        {
            ID = id;
            cts = new CancellationTokenSource();
            pauseEvent = new ManualResetEventSlim(true);
            Time = 0;
            _peers = peers;
            Role = (id == 0 ? Roles.Leader : Roles.Follower); // Defaulting for the sake of doing log replication for now
            // Random timeout between 1.5 and 3 seconds
            electionTimeout = rng.Next(1500, 3000) / 1000.0;
        }

        // Start thread
        public async Task StartMachine()
        {
            try
            {
                if (cts == null)
                {
                    cts = new CancellationTokenSource();
                }
                await Task.Run(() => Clock(cts.Token), cts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Task was gracefully cancelled.");
            }
        }

        // Kill thread
        public void StopMachine()
        {
            cts.Cancel();
        }

        // Pause thread
        public void PauseMachine()
        {
            pauseEvent.Reset();
        }

        // Resume thread
        public void ResumeMachine()
        {
            pauseEvent.Set();
        }

        // Crash
        public void Crash()
        {
            Time = 0;
        }

        // Increment clock every .1 second
        private async Task Clock(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Inbox.Reader.TryRead(out var json))
                {
                    var message = JsonSerializer.Deserialize<Message>(json);
                    if (message != null)
                    {
                        Receive(message);
                    }
                }

                // Raft election logic
                if (Role == Roles.Follower ||  Role == Roles.Candidate)
                {
                    if (Time - lastHeartbeatTime > electionTimeout)
                    {
                        StartElection();
                    }
                }
                else if (Role == Roles.Leader)
                {
                    // Send hearbeat every 0.5 seconds
                    if (Time - lastHeartbeatTime > 0.5)
                    {
                        _ = SendAppendEntries();
                        lastHeartbeatTime = Time;
                    }
                }

                pauseEvent.Wait();
                await Task.Delay(100);
                Time += 0.1;
            }
        }

        // Pass a json message
        public async Task Send(Message message)
        {
            var json = JsonSerializer.Serialize(message);
            var target = _peers.First(p => p.ID == message.To);

            // Simulate delay
            await Task.Delay(rng.Next(minDelay, maxDelay));
            // Simulate message loss
            if (rng.NextDouble() < lossChance)
                return;
            // Then actually send
            await target.Inbox.Writer.WriteAsync(json);
        }

        // Handle messages received from other machines
        public void Receive(Message message)
        {
            // Handle Action
            switch (message.Type)
            {
                case "AppendEntries":
                    // Leader sent entries to append, Followers must handle this
                    HandleAppendEntries(message);
                    break;

                case "AppendEntriesResponse":
                    // Followers replied to leader, Leader must handle this
                    HandleAppendReplies(message);
                    break;

                case "RequestVote":
                    HandleRequestVote(message);
                    break;

                case "RequestVoteResponse":
                    HandleRequestVoteResponse(message);
                    break;

                case "Ping":
                    // Simple Message
                    JsonDocument doc = JsonDocument.Parse(message.PayloadJson);
                    MessageDisplay = doc.RootElement.GetProperty("Text").GetString() ?? "Unknown";
                    break;

                default:
                    Debug.WriteLine($"Machine {ID} received message outside of defined cases");
                    break;
            }
        }

        // Test function to show pinged messages
        public string ShowMessage()
        {
            return MessageDisplay;
        }

        // Leader sends entries for Followers to copy
        public async Task SendAppendEntries()
        {
            // Dummy Data
            LogEntry newEntry = new LogEntry() { Term = CurrentTerm, Index = CommitIndex, Command = "x = 10;" };

            foreach (var follower in _peers.Where(p => p.ID != this.ID))
            {
                var payload = new
                {
                    Term = CurrentTerm,
                    LeaderID = ID,
                    PrevLogIndex = Log.Count - 1,
                    PrevLogTerm = Log.LastOrDefault()?.Term ?? 0,
                    Entries = new List<LogEntry> { newEntry },
                    LeaderCommit = CommitIndex
                };

                var msg = new Message
                {
                    From = ID,
                    To = follower.ID,
                    Type = "AppendEntries",
                    PayloadJson = JsonSerializer.Serialize(payload)
                };

                await Send(msg);
            }
        }

        // Followers respond to Leader's Entries
        public void HandleAppendEntries(Message message)
        {
            JsonDocument doc = JsonDocument.Parse(message.PayloadJson);
            int msgTerm = doc.RootElement.GetProperty("Term").GetInt32();

            // If a leader sends a hearbeat with newer or equal value, acknowledge
            if (msgTerm >= CurrentTerm)
            {
                CurrentTerm = msgTerm;
                // Step down if machine was a candidate
                Role = Roles.Follower;
                lastHeartbeatTime = Time;
                MessageDisplay = $"Heartbeat from Leader (Term {CurrentTerm}).";
            }
        }

        // Leader responds to followers replies
        public void HandleAppendReplies(Message message)
        {
            // @TODO Have Leader commit entries
            MessageDisplay = "Append Entries Replies Case";
        }

        private void StartElection()
        {
            Role = Roles.Candidate;
            ++CurrentTerm;
            // Vote for ourselves and count that vote
            VotedFor = ID;
            votesReceived = 1;
            MessageDisplay = $"Started election for Term {CurrentTerm}.";

            // Reset timer for new election phase
            lastHeartbeatTime = Time;
            electionTimeout = rng.Next(1500, 3000) / 1000.0;

            // Send RequestVote to everyone else
            foreach (var peer in _peers.Where(p => p.ID != this.ID))
            {
                var payload = new { Term = CurrentTerm, CandidateId = ID };
                var msg = new Message
                {
                    From = ID,
                    To = peer.ID,
                    Type = "RequestVote",
                    PayloadJson = JsonSerializer.Serialize(payload)
                };

                _ = Send(msg);
            }
        }

        private void HandleRequestVote(Message message)
        {
            JsonDocument doc = JsonDocument.Parse(message.PayloadJson);
            int msgTerm = doc.RootElement.GetProperty("Term").GetInt32();
            int candidateId = doc.RootElement.GetProperty("CandidateId").GetInt32();

            // If see higher term, step down to Follower
            if (msgTerm > CurrentTerm)
            {
                CurrentTerm = msgTerm;
                Role = Roles.Follower;
                VotedFor = null;
            }

            bool voteGranted = false;

            // Grant vote if term matches and haven't voted for anyone else
            if (msgTerm == CurrentTerm && (VotedFor == null || VotedFor == candidateId))
            {
                voteGranted = true;
                VotedFor = candidateId;
                lastHeartbeatTime = Time;
                MessageDisplay = $"Voted for Machine {candidateId}.";
            }

            // Send the reply
            var replyPayload = new { Term = CurrentTerm, VoteGranted = voteGranted };
            var replyMsg = new Message
            {
                From = ID,
                To = message.From,
                Type = "RequestVoteResponse",
                PayloadJson = JsonSerializer.Serialize(replyPayload)
            };

            _ = Send(replyMsg);
        }

        private void HandleRequestVoteResponse(Message message)
        {
            // If not a candidate, ignore the vote
            if (Role != Roles.Candidate)
            {
                return;
            }

            JsonDocument doc = JsonDocument.Parse(message.PayloadJson);
            int msgTerm = doc.RootElement.GetProperty("Term").GetInt32();
            bool voteGranted = doc.RootElement.GetProperty("VoteGranted").GetBoolean();

            if (msgTerm > CurrentTerm)
            {
                // Step down due to higher term
                CurrentTerm = msgTerm;
                Role = Roles.Follower;
                VotedFor = null;
                return;
            }

            if (voteGranted && msgTerm == CurrentTerm)
            {
                ++votesReceived;
                // Check for majority
                if (votesReceived > _peers.Count / 2.0)
                {
                    // Become leader
                    Role = Roles.Leader;
                    MessageDisplay = $"Elected Leader for Term {CurrentTerm}.";
                    // Force an immediate heartbeat
                    lastHeartbeatTime = 0;
                }
            }
        }

    }
}
