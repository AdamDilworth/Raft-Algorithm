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
        public MachineTile()
        {
            InitializeComponent();
        }

        public int MachineID;

        public void UpdateID()
        {
            lblMachineID.Text = $"Machine: {MachineID}";
        }

        public void UpdateTime(string time)
        {
            lblTime.Text = $"Time: {time}";
        }
    }
}
