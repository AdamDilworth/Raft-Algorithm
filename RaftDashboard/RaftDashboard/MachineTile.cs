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

        private int MachineID;
        private Machine machine;

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
            
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            
        }
    }
}
