using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaftDashboard
{
    internal class Machine
    {
        private CancellationTokenSource cts;
        private string MessageSend;
        private string MessageReceive;

        public int Id { get; set; }
        public double Time { get; set; }
        public event Action? OnTick;

        public Machine(CancellationTokenSource token)
        {
            cts = token;//new CancellationTokenSource();
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

        public async Task SendMessage(string message)
        {
            MessageSend = message;
            // ...
        }

        public async Task GetMessage(string message)
        {
            MessageReceive = message;
            // ...
        }
    }
}
