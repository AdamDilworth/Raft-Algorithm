using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaftDashboard
{
    public class Machine
    {
        // Data for multithreaded and asynchronous behavior
        private readonly CancellationTokenSource cts;
        private ManualResetEventSlim pauseEvent;

        // Message Passing Bus to be implemented
        private string MessageSend;
        private string MessageReceive;

        private readonly int ID;
        public double Time { get; set; }
        public event Action? OnTick;

        public Machine(int id)
        {
            ID = id;
            cts = new CancellationTokenSource();
            pauseEvent = new ManualResetEventSlim(true);
            Time = 0;
        }

        public async Task StartMachine()
        {
            try
            {
                await Task.Run(() => Clock(cts.Token), cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was gracefully cancelled.");
            }
        }

        public void StopMachine()
        {
            cts.Cancel();
        }

        private async Task Clock(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Time: " + Time);
                OnTick?.Invoke();
                await Task.Delay(100);
                Time += 0.1;
            }
        }

        public async Task Send(string message)
        {
            MessageSend = message;
            // ...
        }

        public async Task Recieve(string message)
        {
            MessageReceive = message;
            // ...
        }
    }
}
