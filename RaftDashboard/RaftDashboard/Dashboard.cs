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
                tile.UpdateMachine();
                tile.UpdateUptime();
                tile.UpdateMessage();
                tile.UpdateLogIndex();
                tile.UpdateSharedStateX();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Restart";
            int count = (int)numMachines.Value;

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

            uiTimer.Start();
        }

        private void numMachines_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            if (num.Value % 2 == 0)
            {
                num.Value += 1;
                if (num.Value > num.Maximum) num.Value = num.Maximum;
            }
        }

        private void NetworkSettings_ValueChanged(object sender, EventArgs e)
        {
            if (numMinDelay.Value > numMaxDelay.Value) numMinDelay.Value = numMaxDelay.Value;

            Machine.MinDelay = (int)numMinDelay.Value;
            Machine.MaxDelay = (int)numMaxDelay.Value;

            Machine.LossChance = (double)numLossChance.Value / 100.0;
        }
    }
}
