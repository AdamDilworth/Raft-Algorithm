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

        public void UpdateTime()
        {
            lblTime.Text = $"Time: {(Math.Round((decimal)machine.Time, 2)).ToString()}";
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
            machine.Time = 0;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            machine.ResumeMachine();
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (machine.Role == Machine.Roles.Leader)
            {
                await machine.SendAppendEntries();
            }
            else
            {
                using (var prompt = new TargetPromptForm())
                {
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        int targetId = prompt.TargetID;
                        // send message
                        var payload = new
                        {
                            Text = $"Test String from machine {MachineID}"
                        };
                        Message msg = new Message()
                        {
                            From = MachineID,
                            To = targetId,
                            Type = "Ping",
                            Payload = JsonSerializer.SerializeToElement(payload)
                        };

                        _ = machine.Send(msg);
                    }
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
