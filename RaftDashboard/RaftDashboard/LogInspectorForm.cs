using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace RaftDashboard
{
    public partial class LogInspectorForm : Form
    {
        private readonly Machine _machine;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        public LogInspectorForm(Machine machine, int machineId)
        {
            InitializeComponent();
            _machine = machine;

            this.Text = $"Machine {machineId} - Live Log Inspector";
            this.Width = 475;
            this.Height = 500;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 500;
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            RefreshGrid();
        }

        private void RefreshTimer_Tick(object? sender,  EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var logSnapshot = _machine.Log.ToList();

            if (dataGridView1.DataSource == null || ((System.Collections.IList)dataGridView1.DataSource).Count != logSnapshot.Count)
            {
                dataGridView1.DataSource = logSnapshot;

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            base.OnFormClosing(e);
        }
    }
}
