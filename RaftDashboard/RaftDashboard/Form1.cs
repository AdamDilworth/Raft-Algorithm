namespace RaftDashboard
{
    public partial class Dashboard : Form
    {
        
        private List<Machine> machines = new();
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

            machines.Clear();
            machineTiles.Clear();
            flpMachineInfo.Controls.Clear();

            for (int i = 0; i < count; i++)
            {

                // Create machine and add it to new tile
                var machine = new Machine(i, machines);
                var tile = new MachineTile(machine, i);
                machines.Add(machine);
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
