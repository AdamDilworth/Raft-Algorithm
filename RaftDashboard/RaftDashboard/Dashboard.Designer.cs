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
            lblMinutes = new Label();
            numMinutes = new NumericUpDown();
            btnStart = new Button();
            lblMachines = new Label();
            pnlControl = new Panel();
            tlpDashboard = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numMachines).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            pnlControl.SuspendLayout();
            tlpDashboard.SuspendLayout();
            SuspendLayout();
            // 
            // flpMachineInfo
            // 
            flpMachineInfo.AutoScroll = true;
            flpMachineInfo.Dock = DockStyle.Fill;
            flpMachineInfo.Location = new Point(3, 83);
            flpMachineInfo.Name = "flpMachineInfo";
            flpMachineInfo.Size = new Size(794, 364);
            flpMachineInfo.TabIndex = 7;
            // 
            // numMachines
            // 
            numMachines.Location = new Point(165, 28);
            numMachines.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numMachines.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMachines.Name = "numMachines";
            numMachines.Size = new Size(150, 27);
            numMachines.TabIndex = 5;
            numMachines.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Location = new Point(321, 30);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(108, 20);
            lblMinutes.TabIndex = 4;
            lblMinutes.Text = "Minutes to Run";
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(435, 28);
            numMinutes.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(150, 27);
            numMinutes.TabIndex = 6;
            numMinutes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStart.Location = new Point(672, 28);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start!";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lblMachines
            // 
            lblMachines.AutoSize = true;
            lblMachines.Location = new Point(12, 30);
            lblMachines.Name = "lblMachines";
            lblMachines.Size = new Size(147, 20);
            lblMachines.TabIndex = 2;
            lblMachines.Text = "Number of Machines";
            // 
            // pnlControl
            // 
            pnlControl.Controls.Add(lblMachines);
            pnlControl.Controls.Add(btnStart);
            pnlControl.Controls.Add(numMinutes);
            pnlControl.Controls.Add(lblMinutes);
            pnlControl.Controls.Add(numMachines);
            pnlControl.Dock = DockStyle.Fill;
            pnlControl.Location = new Point(3, 3);
            pnlControl.Name = "pnlControl";
            pnlControl.Size = new Size(794, 74);
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
            tlpDashboard.Name = "tlpDashboard";
            tlpDashboard.RowCount = 2;
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDashboard.Size = new Size(800, 450);
            tlpDashboard.TabIndex = 0;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tlpDashboard);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RAFT Dashboard";
            ((System.ComponentModel.ISupportInitialize)numMachines).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            pnlControl.ResumeLayout(false);
            pnlControl.PerformLayout();
            tlpDashboard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel flpMachineInfo;
        private NumericUpDown numMachines;
        private Label lblMinutes;
        private NumericUpDown numMinutes;
        private Button btnStart;
        private Label lblMachines;
        private Panel pnlControl;
        private TableLayoutPanel tlpDashboard;
    }
}
