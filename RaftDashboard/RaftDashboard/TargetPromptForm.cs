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
    public partial class TargetPromptForm : Form
    {
        public TargetPromptForm()
        {
            InitializeComponent();
        }

        public int TargetID => (int)numericUpDown1.Value;
    }
}
