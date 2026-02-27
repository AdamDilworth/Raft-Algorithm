using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public void UpdateTime(string time)
        {
            lblTime.Text = $"Time: {time}";
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
            machine.StopMachine();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            machine.ResumeMachine();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            using (var prompt = new TargetPromptForm())
            {
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    int targetId = prompt.TargetID;
                    // send message
                    Message msg = new Message() { From = MachineID, To = targetId, Type = "Ping", Payload = $"Test String from machine {MachineID}" };
                    _ = machine.Send(msg);
                }
            }
        }

        private void btnViewMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                            $"{machine.ShowMessage()}",
                            $"Machine {MachineID}'s message"
                            );
        }
    }
}
