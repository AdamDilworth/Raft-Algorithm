using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaftDashboard
{
    public partial class MachineTile : UserControl
    {
        public MachineTile(Machine machine, int id)
        {
            InitializeComponent();
            this.machine = machine;
            MachineID = id;
            lblMachineID.Text = $"Machine: {MachineID}";
        }

        private readonly int MachineID;
        private readonly Machine machine;

        public void UpdateMachine()
        {
            lblMachineID.Text = $"Machine: {MachineID} ({machine.Role})";

            if (machine.Role == Machine.Roles.Leader)
            {
                this.BackColor = Color.LightGreen;
            }
            else if (machine.Role == Machine.Roles.Candidate)
            {
                this.BackColor = Color.LightSalmon;
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }
        }

        public void UpdateUptime()
        {
            lblUptime.Text = $"Uptime: {Math.Round((decimal)machine.Time, 1)} seconds";
        }

        public void UpdateMessage()
        {
            lblMessage.Text = $"Message: {machine.ShowMessage()}";
        }

        public void UpdateLogIndex()
        {
            lblLogIndex.Text = $"Log Index: {machine.Log.Count - 1}";
        }

        public void UpdateSharedStateX()
        {
            lblSharedStateX.Text = $"Shared State (X): {machine.SharedStateX}";
        }

        public void Start()
        {
            _ = machine.StartMachine();
        }
        public void Stop()
        {
            machine.StopMachine();
        }

        private void btnInterrupt_Click(object sender, EventArgs e)
        {
            machine.PauseMachine();
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            machine.Crash();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            machine.ResumeMachine();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (machine.Role == Machine.Roles.Leader)
            {
                using (var prompt = new CommandPromptForm())
                {
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        machine.AddClientCommand(prompt.CommandString);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Machine {MachineID} is a Follower. Clients must redirect requests to the Leader.", "Redirect Required");
            }
        }
    }
}
