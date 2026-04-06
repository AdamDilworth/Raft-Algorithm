namespace RaftDashboard
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flpMachineInfo = new FlowLayoutPanel();
            numMachines = new NumericUpDown();
            btnStart = new Button();
            lblMachines = new Label();
            pnlControl = new Panel();
            tlpDashboard = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numMachines).BeginInit();
            pnlControl.SuspendLayout();
            tlpDashboard.SuspendLayout();
            SuspendLayout();
            // 
            // flpMachineInfo
            // 
            flpMachineInfo.AutoScroll = true;
            flpMachineInfo.Dock = DockStyle.Fill;
            flpMachineInfo.Location = new Point(4, 104);
            flpMachineInfo.Margin = new Padding(4);
            flpMachineInfo.Name = "flpMachineInfo";
            flpMachineInfo.Size = new Size(992, 454);
            flpMachineInfo.TabIndex = 7;
            // 
            // numMachines
            // 
            numMachines.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            numMachines.Location = new Point(206, 35);
            numMachines.Margin = new Padding(4);
            numMachines.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            numMachines.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMachines.Name = "numMachines";
            numMachines.Size = new Size(188, 31);
            numMachines.TabIndex = 5;
            numMachines.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numMachines.ValueChanged += numMachines_ValueChanged;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStart.Location = new Point(840, 35);
            btnStart.Margin = new Padding(4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(118, 36);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lblMachines
            // 
            lblMachines.AutoSize = true;
            lblMachines.Location = new Point(15, 38);
            lblMachines.Margin = new Padding(4, 0, 4, 0);
            lblMachines.Name = "lblMachines";
            lblMachines.Size = new Size(178, 25);
            lblMachines.TabIndex = 2;
            lblMachines.Text = "Number of Machines";
            // 
            // pnlControl
            // 
            pnlControl.Controls.Add(lblMachines);
            pnlControl.Controls.Add(btnStart);
            pnlControl.Controls.Add(numMachines);
            pnlControl.Dock = DockStyle.Fill;
            pnlControl.Location = new Point(4, 4);
            pnlControl.Margin = new Padding(4);
            pnlControl.Name = "pnlControl";
            pnlControl.Size = new Size(992, 92);
            pnlControl.TabIndex = 8;
            // 
            // tlpDashboard
            // 
            tlpDashboard.ColumnCount = 1;
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDashboard.Controls.Add(pnlControl, 0, 0);
            tlpDashboard.Controls.Add(flpMachineInfo, 0, 1);
            tlpDashboard.Dock = DockStyle.Fill;
            tlpDashboard.Location = new Point(0, 0);
            tlpDashboard.Margin = new Padding(4);
            tlpDashboard.Name = "tlpDashboard";
            tlpDashboard.RowCount = 2;
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDashboard.Size = new Size(1000, 562);
            tlpDashboard.TabIndex = 0;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(tlpDashboard);
            Margin = new Padding(4);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RAFT Dashboard";
            ((System.ComponentModel.ISupportInitialize)numMachines).EndInit();
            pnlControl.ResumeLayout(false);
            pnlControl.PerformLayout();
            tlpDashboard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel flpMachineInfo;
        private NumericUpDown numMachines;
        private Button btnStart;
        private Label lblMachines;
        private Panel pnlControl;
        private TableLayoutPanel tlpDashboard;
    }
}
