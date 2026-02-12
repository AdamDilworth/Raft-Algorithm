// Machine class
using System;
using System.Threading;
using System.Threading.Tasks;
public class Machine
{
    private CancellationTokenSource cts;
    private double time;

    public Machine()
    {
        cts = new CancellationTokenSource();
        time = 0;
    }

    public async Task startMachine()
    {
        try
        {
            await Task.Run(() => clock(cts.Token), cts.Token);
        }
        catch (OperationCanceledException)
        {
                Console.WriteLine("Task was gracefully cancelled.");
        }
    }

    public void stopMachine()
    {
        cts.Cancel();
    }

    private async Task clock(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {
            Console.WriteLine("Time: " + time);
            await Task.Delay(100);
            time += 0.1;
        }
    }
}
