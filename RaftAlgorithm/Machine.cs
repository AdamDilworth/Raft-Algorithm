// Machine class
using System;
using System.Threading;
using System.Threading.Tasks;
public class Machine
{
    private CancellationTokenSource cts;
    private double time;
    private int id;

    // constructor
    public Machine(int id)
    {
        cts = new CancellationTokenSource();
        time = 0;
        this.id = id;
    }

    // Start thread
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

    // Kills thread
    public void stopMachine()
    {
        cts.Cancel();
    }

    // Increment clock every .1 second and print
    private async Task clock(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {
            Console.WriteLine("Thread: " + id + " Time: " + time);
            await Task.Delay(100);
            time += 0.1;
        }
    }
}
