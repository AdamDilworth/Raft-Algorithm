namespace RaftDashboard
{
    public partial class Dashboard : Form
    {
        //private List<Machine> machines = new();
        //private List<Task> tasks = new();
        //private List<CancellationTokenSource> tokens = new();

        private List<MachineTile> machineTiles = new();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Restart";
            int count = (int)numMachines.Value;
            int timeout = (int)numMinutes.Value;

            flpMachineInfo.Controls.Clear();

            for (int i = 0; i < count; i++)
            {

                // Create machine and add it to new tile
                var machine = new Machine(i + 1);
                var tile = new MachineTile(machine, i + 1);
                machineTiles.Add(tile);
                flpMachineInfo.Controls.Add(tile);

                // Connect the OnTick event with updating the time on screen
                machine.OnTick += () =>
                {
                    Invoke(() => tile.UpdateTime(machine.Time.ToString()));
                };

                // Add tasks and machines to lists
                tile.Start();
            }

            // Wait for time specified
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(timeout));
                foreach (var t in machineTiles)
                    t.Stop();
            });
        }
    }
}
