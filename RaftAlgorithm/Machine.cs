// Machine class
using System;
using System.Threading;
using System.Threading.Tasks;
public class Machine
{
    private CancellationTokenSource cts;
    private ManualResetEventSlim pauseEvent;
    private double time;
    private int id;

    // constructor
    public Machine(int id)
    {
        cts = new CancellationTokenSource();
        // default state "true" is not paused
        pauseEvent = new ManualResetEventSlim(true);
        time = 0;
        this.id = id;
    }

    // Start thread
    public async Task startMachine()
    {
        try
        {
            await Task.Run(() => clock(), cts.Token);
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

    // Pause thread
    public void pauseMachine()
    {
        pauseEvent.Reset();
    }

    // Resume thread
    public void resumeMachine()
    {
        pauseEvent.Set();
    }

    // Increment clock every .1 second and print
    private async Task clock()
    {
        while(!cts.Token.IsCancellationRequested)
        {
            pauseEvent.Wait();
            Console.WriteLine("Thread: " + id + " Time: " + time);
            await Task.Delay(100);
            time += 0.1;
        }
    }
}
