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
            btnStart = new Button();
            lblMachines = new Label();
            lblMinutes = new Label();
            numMachines = new NumericUpDown();
            numMinutes = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numMachines).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(675, 398);
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
            lblMachines.Location = new Point(12, 35);
            lblMachines.Name = "lblMachines";
            lblMachines.Size = new Size(147, 20);
            lblMachines.TabIndex = 2;
            lblMachines.Text = "Number of Machines";
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Location = new Point(321, 35);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(108, 20);
            lblMinutes.TabIndex = 4;
            lblMinutes.Text = "Minutes to Run";
            // 
            // numMachines
            // 
            numMachines.Location = new Point(165, 33);
            numMachines.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numMachines.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMachines.Name = "numMachines";
            numMachines.Size = new Size(150, 27);
            numMachines.TabIndex = 5;
            numMachines.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(435, 33);
            numMinutes.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(150, 27);
            numMinutes.TabIndex = 6;
            numMinutes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(29, 79);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(740, 313);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numMinutes);
            Controls.Add(numMachines);
            Controls.Add(lblMinutes);
            Controls.Add(lblMachines);
            Controls.Add(btnStart);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RAFT Dashboard";
            ((System.ComponentModel.ISupportInitialize)numMachines).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Label lblMachines;
        private Label lblMinutes;
        private NumericUpDown numMachines;
        private NumericUpDown numMinutes;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
