namespace RaftDashboard
{
    public partial class Dashboard : Form
    {
        private List<Machine> machines = new();
        private List<Task> tasks = new();
        private List<CancellationTokenSource> tokens = new();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Restart";
            int count = (int)numMachines.Value;
            int timeout = (int)numMinutes.Value;

            machines.Clear();
            tasks.Clear();
            tokens.Clear();
            flpMachineInfo.Controls.Clear();

            for (int i = 0; i < count; i++)
            {
                // Create token to run each thread
                var cts = new CancellationTokenSource();
                tokens.Add(cts);
                // Create machine and add it to new tile
                var machine = new Machine(cts) { Id = i };
                var tile = new MachineTile { MachineID = i };
                tile.UpdateID();
                flpMachineInfo.Controls.Add(tile);

                // Update display after machine updates its time
                machine.OnTick += () =>
                {
                    Invoke(() => tile.UpdateTime(machine.Time.ToString()));
                };

                // Add tasks and machines to lists
                var task = Task.Run(() => machine.StartMachine());
                tasks.Add(task);
                machines.Add(machine);
            }

            // Stop after timeout
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(timeout));
                foreach (var t in tokens)
                    t.Cancel();
            });
        }
    }
}
