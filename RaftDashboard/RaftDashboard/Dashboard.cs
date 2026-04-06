namespace RaftDashboard
{
    public partial class Dashboard : Form
    {
        
        private List<Machine> machines = new();
        private List<MachineTile> machineTiles = new();
        private System.Windows.Forms.Timer uiTimer;

        public Dashboard()
        {
            InitializeComponent();

            // Initialize UI timer to tick every 100ms
            uiTimer = new System.Windows.Forms.Timer();
            uiTimer.Interval = 100;
            uiTimer.Tick += UiTimer_Tick;
        }

        private void UiTimer_Tick(object? sender, EventArgs e)
        {
            // UI thread updates itself without background threads
            foreach (var tile in machineTiles)
            {
                tile.UpdateTime();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Restart";
            int count = (int)numMachines.Value;
            int timeout = (int)numMinutes.Value;

            // Stop existing machines
            foreach (var m in machines)
            {
                m.StopMachine();
            }
            foreach (Control control in flpMachineInfo.Controls)
            {
                control.Dispose();
            }

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

            uiTimer.Start();
        }
    }
}
