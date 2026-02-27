using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RaftDashboard
{
    public class Machine
    {
        // Data for multithreaded and asynchronous behavior
        private CancellationTokenSource cts;
        private ManualResetEventSlim pauseEvent;

        // Message Passing Bus
        public Channel<string> Inbox { get; } = Channel.CreateUnbounded<string>();
        private readonly List<Machine> _peers;
        private string currentMsg = "";

        private readonly int ID;
        public double Time { get; set; }
        public event Action? OnTick;

        // Constructor
        public Machine(int id, List<Machine> peers)
        {
            ID = id;
            cts = new CancellationTokenSource();
            pauseEvent = new ManualResetEventSlim(true);
            Time = 0;
            _peers = peers;
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
                Console.WriteLine("Task was gracefully cancelled.");
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

        // Increment clock every .1 second
        private async Task Clock(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                //Console.WriteLine("Thread: " + id + " Time: " + time);
                if (Inbox.Reader.TryRead(out var json))
                {
                    var message = JsonSerializer.Deserialize<Message>(json);
                    Receive(message);
                }
                pauseEvent.Wait();
                OnTick?.Invoke();
                await Task.Delay(100);
                Time += 0.1;
            }
        }

        public async Task Send(Message message)
        {
            var json = JsonSerializer.Serialize(message);
            var target = _peers.First(p => p.ID == message.To);
            await target.Inbox.Writer.WriteAsync(json);
        }

        public void Receive(Message message)
        {
            // Handle Message
            currentMsg = message.Payload;
        }

        // Test function
        public string ShowMessage()
        {
            return currentMsg;
        }
    }
}
