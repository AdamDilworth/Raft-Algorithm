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

        // I/O
        private readonly string stateFilePath;

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
        private int LastApplied = 0;
        private double lastHeartbeatTime = 0;
        private double electionTimeout;
        private int? VotedFor = null;
        private int votesReceived = 0;
        private Dictionary<int, int> nextIndex = new(); // Tracks what the next log entry to send to that follower is
        private Dictionary<int, int> matchIndex = new(); // Tracks the highest log entry known to be replicated on that follower
        public int SharedStateX { get; private set; } = 0;

        // Data for multithreaded and asynchronous behavior
        private CancellationTokenSource cts;

        // Message Passing Bus
        public Channel<string> Inbox { get; } = Channel.CreateUnbounded<string>();
        private readonly List<Machine> _peers;
        private string MessageDisplay = "";

        // Log
        public IReadOnlyList<LogEntry> Log => _log.AsReadOnly();
        private readonly List<LogEntry> _log = [ new LogEntry { Term = 0, Index = 0, Command = "INIT" } ];

        // Stopwatch and Event
        public double Time { get; private set; }
        public bool IsNetworkConnected { get; private set; } = true;

        /* Methods */

        // Constructor
        public Machine(int id, List<Machine> peers)
        {
            ID = id;
            cts = new CancellationTokenSource();
            Time = 0;
            _peers = peers;
            Role = Roles.Follower;

            // Persistence
            stateFilePath = $"Machine_{ID}_State.json";

            _log.Clear();
            _log.Add(new LogEntry { Term = 0, Index = 0, Command = "INIT" });

            LoadState();

            // Random timeout between 1.5 and 3 seconds
            electionTimeout = rng.Next(1500, 3000) / 1000.0;
        }

        // Save a state to a JSON file
        private void SaveState()
        {
            var state = new PersistentState
            {
                CurrentTerm = this.CurrentTerm,
                VotedFor = this.VotedFor,
                Log = this._log
            };

            System.IO.File.WriteAllText(stateFilePath, JsonSerializer.Serialize(state));
        }

        // Load a state to a JSON file
        private void LoadState()
        {
            if (System.IO.File.Exists(stateFilePath))
            {
                try
                {
                    var json = System.IO.File.ReadAllText(stateFilePath);
                    var state = JsonSerializer.Deserialize<PersistentState>(json);
                    if (state != null)
                    {
                        CurrentTerm = state.CurrentTerm;
                        VotedFor = state.VotedFor;
                        _log.Clear();
                        _log.AddRange(state.Log);
                        MessageDisplay = $"Loaded state from disk (Term {CurrentTerm}).";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load state from Machine {ID}: {ex.Message}");
                }
            }
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
        public void SeverNetwork()
        {
            IsNetworkConnected = false;
            MessageDisplay = "Network Connection Severed.";
        }

        // Resume thread
        public void RestoreNetwork()
        {
            IsNetworkConnected = true;
            lastHeartbeatTime = Time;
            MessageDisplay = "Network Connection Restored.";
        }

        // Crash
        public void Crash()
        {
            // Simulate total wipe
            Time = 0;
            lastHeartbeatTime = 0;
            Role = Roles.Follower;
            CommitIndex = 0;
            LastApplied = 0;
            SharedStateX = 0;
            MessageDisplay = "Crashed.";

            // Clear out the inbox
            while (Inbox.Reader.TryRead(out _)) { }

            _log.Clear();
            VotedFor = null;
            CurrentTerm = 0;

            LoadState();

            MessageDisplay = "Crashed and rebooted from disk.";
        }

        // Increment clock every .1 second
        private async Task Clock(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (Inbox.Reader.TryRead(out var json))
                {
                    if (!IsNetworkConnected) continue;

                    var message = JsonSerializer.Deserialize<Message>(json);
                    if (message != null)
                    {
                        try
                        {
                            Receive(message);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Machine {ID} crashed processing message: {ex.Message}");
                        }
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

                await Task.Delay(100);
                Time += 0.1;
            }
        }

        // Pass a json message
        public async Task Send(Message message)
        {
            if (!IsNetworkConnected) return;

            var json = JsonSerializer.Serialize(message);
            var target = _peers.First(p => p.ID == message.To);

            // Simulate delay
            await Task.Delay(rng.Next(minDelay, maxDelay));
            // Simulate message loss
            if (rng.NextDouble() < lossChance) return;
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
            foreach (var follower in _peers.Where(p => p.ID != this.ID))
            {
                // Default to current log count
                int nextIdx = nextIndex.ContainsKey(follower.ID) ? nextIndex[follower.ID] : Log.Count;

                int prevLogIndex = nextIdx - 1;
                int prevLogTerm = 0;

                // Get term from previous log entry
                if (prevLogIndex >= 0 && prevLogIndex < Log.Count)
                {
                    prevLogTerm = Log[prevLogIndex].Term;
                }

                // Grab all necessary entries
                List<LogEntry> entriesToSend = new();
                if (nextIdx < Log.Count)
                {
                    entriesToSend = _log.GetRange(nextIdx, Log.Count - nextIdx);
                }

                var payload = new
                {
                    Term = CurrentTerm,
                    LeaderID = ID,
                    PrevLogIndex = prevLogIndex,
                    PrevLogTerm = prevLogTerm,
                    Entries = entriesToSend,
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
            var root = doc.RootElement;

            int msgTerm = root.GetProperty("Term").GetInt32();
            int leaderId = root.GetProperty("LeaderID").GetInt32();
            int prevLogIndex = root.GetProperty("PrevLogIndex").GetInt32();
            int prevLogTerm = root.GetProperty("PrevLogTerm").GetInt32();
            int leaderCommit = root.GetProperty("LeaderCommit").GetInt32();

            // Reply false if term < currentTerm
            if (msgTerm < CurrentTerm)
            {
                SendAppendReply(message.From, false);
                return;
            }

            CurrentTerm = msgTerm;
            Role = Roles.Follower;
            lastHeartbeatTime = Time;
            MessageDisplay = $"Following M{leaderId} (Term {CurrentTerm}).";

            // Reply false if log mismatch
            if (Log.Count <= prevLogIndex || Log[prevLogIndex].Term != prevLogTerm)
            {
                SendAppendReply(message.From, false);
                return;
            }

            // Logs match. Overwrite conflicts, append new ones
            bool logChanged = false;
            var entriesProp = root.GetProperty("Entries");
            int insertIndex = prevLogIndex + 1;

            foreach (var entryElement in entriesProp.EnumerateArray())
            {
                var entry = JsonSerializer.Deserialize<LogEntry>(entryElement.GetRawText());
                if (entry != null)
                {
                    if (insertIndex < Log.Count)
                    {
                        // Conflict. Overwrite the log
                        _log.RemoveRange(insertIndex, _log.Count - insertIndex);
                        logChanged = true;
                    }
                    _log.Add(entry);
                    logChanged = true;
                    ++insertIndex;
                }
            }

            if (logChanged) SaveState();

            // Update commit index
            if (leaderCommit > CommitIndex)
            {
                CommitIndex = Math.Min(leaderCommit, Log.Count - 1);
                MessageDisplay = $"Committed up to {CommitIndex}.";
            }

            if (leaderCommit < CommitIndex)
            {
                CommitIndex = Math.Min(leaderCommit, Log.Count - 1);
                MessageDisplay = $"Committed up to {CommitIndex}.";
            }
            ApplyCommittedLogs();

            // Success
            SendAppendReply(message.From, true);

        }

        // Leader responds to followers replies
        public void HandleAppendReplies(Message message)
        {
            if (Role != Roles.Leader)
            {
                return;
            }

            JsonDocument doc = JsonDocument.Parse(message.PayloadJson);
            int msgTerm = doc.RootElement.GetProperty("Term").GetInt32();
            bool success = doc.RootElement.GetProperty("Success").GetBoolean();
            int followerMatchIndex = doc.RootElement.GetProperty("MatchIndex").GetInt32();

            if (msgTerm > CurrentTerm)
            {
                CurrentTerm = msgTerm;
                Role = Roles.Follower;
                return;
            }

            int followerId = message.From;

            if (success)
            {
                // Update tracking
                nextIndex[followerId] = followerMatchIndex + 1;
                matchIndex[followerId] = followerMatchIndex;

                // Check for Consensus
                // Look for highest index that a mahority of nodes have replicated
                bool committedNew = false;
                for (int N = Log.Count - 1; N > CommitIndex; --N)
                {
                    if (Log[N].Term == CurrentTerm)
                    {
                        int replicationCount = 1;
                        foreach (var peer in _peers.Where(p => p.ID != this.ID))
                        {
                            if (matchIndex.ContainsKey(peer.ID) && matchIndex[peer.ID] >= N)
                            {
                                ++replicationCount;
                            }
                        }

                        if (replicationCount > _peers.Count / 2.0)
                        {
                            CommitIndex = N;
                            MessageDisplay = $"Consensus Reached. Committed Index {CommitIndex}";
                            committedNew = true;
                            // Found the highest committable index
                            break;
                        }
                    }
                }

                // Execute the logs
                if (committedNew)
                {
                    ApplyCommittedLogs();
                    _ = SendAppendEntries();
                }
            }
            else
            {
                // Inconsistent. Backup a step and try again next hearbeat
                if (nextIndex.ContainsKey(followerId) && nextIndex[followerId] > 1)
                {
                    --nextIndex[followerId];
                }
            }
        }

        private void StartElection()
        {
            Role = Roles.Candidate;
            ++CurrentTerm;
            // Vote for ourselves and count that vote
            VotedFor = ID;
            votesReceived = 1;
            MessageDisplay = $"Started election for Term {CurrentTerm}.";

            SaveState();

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

                SaveState();
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

                    // Initialize leader state tracking
                    nextIndex.Clear();
                    matchIndex.Clear();
                    foreach (var peer in _peers.Where(p => p.ID != this.ID))
                    {
                        // Initially set to the leader's next empty slot
                        nextIndex[peer.ID] = Log.Count;
                        // Assume no replication yet
                        matchIndex[peer.ID] = 0;
                    }
                }
            }
        }

        private void SendAppendReply(int leaderId, bool success)
        {
            var replyPayload = new { Term = CurrentTerm, Success = success, MatchIndex = Log.Count - 1 };
            var replyMsg = new Message
            {
                From = ID,
                To = leaderId,
                Type = "AppendEntriesResponse",
                PayloadJson = JsonSerializer.Serialize(replyPayload)
            };

            _ = Send(replyMsg);
        }

        public void AddClientCommand(string command)
        {
            if (Role == Roles.Leader)
            {
                _log.Add(new LogEntry { Term = CurrentTerm, Index = Log.Count, Command = command });
                SaveState();
                MessageDisplay = $"Added command: {command}";
            }
        }

        private void ApplyCommittedLogs()
        {
            // Run logs that have not been executed yet
            while (LastApplied < CommitIndex)
            {
                ++LastApplied;
                string command = Log[LastApplied].Command;

                // Parse the command
                string[] parts = command.Split(' ');
                if (parts.Length == 3 && parts[1] == "X")
                {
                    if (int.TryParse(parts[2], out int val))
                    {
                        switch (parts[0])
                        {
                            case "SET": SharedStateX = val; break;
                            case "ADD": SharedStateX += val; break;
                            case "SUBTRACT": SharedStateX -= val; break;
                            case "MULTIPLY": SharedStateX *= val; break;
                        }
                    }
                }
            }
        }

    }
}
