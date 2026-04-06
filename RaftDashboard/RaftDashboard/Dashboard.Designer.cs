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
            numLossChance = new NumericUpDown();
            label3 = new Label();
            numMaxDelay = new NumericUpDown();
            label2 = new Label();
            numMinDelay = new NumericUpDown();
            label1 = new Label();
            tlpDashboard = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numMachines).BeginInit();
            pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLossChance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinDelay).BeginInit();
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
            numMachines.Size = new Size(72, 31);
            numMachines.TabIndex = 5;
            numMachines.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numMachines.ValueChanged += numMachines_ValueChanged;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStart.Location = new Point(865, 32);
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
            pnlControl.Controls.Add(numLossChance);
            pnlControl.Controls.Add(label3);
            pnlControl.Controls.Add(numMaxDelay);
            pnlControl.Controls.Add(label2);
            pnlControl.Controls.Add(numMinDelay);
            pnlControl.Controls.Add(label1);
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
            // numLossChance
            // 
            numLossChance.Location = new Point(779, 35);
            numLossChance.Name = "numLossChance";
            numLossChance.Size = new Size(72, 31);
            numLossChance.TabIndex = 11;
            numLossChance.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numLossChance.ValueChanged += NetworkSettings_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(643, 37);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(129, 25);
            label3.TabIndex = 10;
            label3.Text = "Loss Chance %";
            // 
            // numMaxDelay
            // 
            numMaxDelay.Location = new Point(564, 35);
            numMaxDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxDelay.Name = "numMaxDelay";
            numMaxDelay.Size = new Size(72, 31);
            numMaxDelay.TabIndex = 9;
            numMaxDelay.Value = new decimal(new int[] { 300, 0, 0, 0 });
            numMaxDelay.ValueChanged += NetworkSettings_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(463, 37);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(94, 25);
            label2.TabIndex = 8;
            label2.Text = "Max Delay";
            // 
            // numMinDelay
            // 
            numMinDelay.Location = new Point(384, 35);
            numMinDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMinDelay.Name = "numMinDelay";
            numMinDelay.Size = new Size(72, 31);
            numMinDelay.TabIndex = 7;
            numMinDelay.Value = new decimal(new int[] { 150, 0, 0, 0 });
            numMinDelay.ValueChanged += NetworkSettings_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(286, 38);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 6;
            label1.Text = "Min Delay";
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
            ((System.ComponentModel.ISupportInitialize)numLossChance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinDelay).EndInit();
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
        private Label label1;
        private NumericUpDown numMinDelay;
        private Label label2;
        private NumericUpDown numMaxDelay;
        private Label label3;
        private NumericUpDown numLossChance;
    }
}
