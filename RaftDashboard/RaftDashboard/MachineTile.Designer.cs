namespace RaftDashboard
{
    partial class MachineTile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMachineID = new Label();
            lblTime = new Label();
            btnSendMessage = new Button();
            btnInterrupt = new Button();
            btnCrash = new Button();
            btnViewMessage = new Button();
            SuspendLayout();
            // 
            // lblMachineID
            // 
            lblMachineID.AutoSize = true;
            lblMachineID.Location = new Point(20, 17);
            lblMachineID.Name = "lblMachineID";
            lblMachineID.Size = new Size(65, 20);
            lblMachineID.TabIndex = 0;
            lblMachineID.Text = "Machine";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(22, 50);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(49, 20);
            lblTime.TabIndex = 1;
            lblTime.Text = "Time: ";
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(22, 87);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(117, 29);
            btnSendMessage.TabIndex = 2;
            btnSendMessage.Text = "Send Message";
            btnSendMessage.UseVisualStyleBackColor = true;
            // 
            // btnInterrupt
            // 
            btnInterrupt.Location = new Point(400, 17);
            btnInterrupt.Name = "btnInterrupt";
            btnInterrupt.Size = new Size(94, 29);
            btnInterrupt.TabIndex = 3;
            btnInterrupt.Text = "Interrupt";
            btnInterrupt.UseVisualStyleBackColor = true;
            btnInterrupt.Click += btnInterrupt_Click;
            // 
            // btnCrash
            // 
            btnCrash.Location = new Point(400, 52);
            btnCrash.Name = "btnCrash";
            btnCrash.Size = new Size(94, 29);
            btnCrash.TabIndex = 4;
            btnCrash.Text = "Crash";
            btnCrash.UseVisualStyleBackColor = true;
            btnCrash.Click += btnCrash_Click;
            // 
            // btnViewMessage
            // 
            btnViewMessage.Location = new Point(145, 87);
            btnViewMessage.Name = "btnViewMessage";
            btnViewMessage.Size = new Size(123, 29);
            btnViewMessage.TabIndex = 5;
            btnViewMessage.Text = "View Message";
            btnViewMessage.UseVisualStyleBackColor = true;
            // 
            // MachineTile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnViewMessage);
            Controls.Add(btnCrash);
            Controls.Add(btnInterrupt);
            Controls.Add(btnSendMessage);
            Controls.Add(lblTime);
            Controls.Add(lblMachineID);
            Name = "MachineTile";
            Size = new Size(515, 134);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMachineID;
        private Label lblTime;
        private Button btnSendMessage;
        private Button btnInterrupt;
        private Button btnCrash;
        private Button btnViewMessage;
    }
}
