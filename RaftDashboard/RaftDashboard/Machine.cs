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

                if (Role == Roles.Follower ||  Role == Roles.Candidate)
                {
                    if (Time - lastHeartbeatTime > electionTimeout)
                    {
                        StartElection();
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
            lastHeartbeatTime = Time;
            MessageDisplay = "Received Heartbeat/Entries";
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
            MessageDisplay = $"Started election for Term {CurrentTerm}.";

            // Reset timer for new election phase
            lastHeartbeatTime = Time;
            electionTimeout = rng.Next(1500, 3000) / 1000.0;

            // @TODO Send RequestVote message to peers
        }

    }
}
