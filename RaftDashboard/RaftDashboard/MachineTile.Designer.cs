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
            btnResume = new Button();
            SuspendLayout();
            // 
            // lblMachineID
            // 
            lblMachineID.Location = new Point(25, 21);
            lblMachineID.Margin = new Padding(4, 0, 4, 0);
            lblMachineID.Name = "lblMachineID";
            lblMachineID.Size = new Size(212, 31);
            lblMachineID.TabIndex = 0;
            lblMachineID.Text = "Machine";
            // 
            // lblTime
            // 
            lblTime.Location = new Point(28, 62);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(225, 31);
            lblTime.TabIndex = 1;
            lblTime.Text = "Time: ";
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(28, 109);
            btnSendMessage.Margin = new Padding(4, 4, 4, 4);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(146, 36);
            btnSendMessage.TabIndex = 2;
            btnSendMessage.Text = "Send Message";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // btnInterrupt
            // 
            btnInterrupt.Location = new Point(500, 21);
            btnInterrupt.Margin = new Padding(4, 4, 4, 4);
            btnInterrupt.Name = "btnInterrupt";
            btnInterrupt.Size = new Size(118, 36);
            btnInterrupt.TabIndex = 3;
            btnInterrupt.Text = "Interrupt";
            btnInterrupt.UseVisualStyleBackColor = true;
            btnInterrupt.Click += btnInterrupt_Click;
            // 
            // btnCrash
            // 
            btnCrash.Location = new Point(500, 65);
            btnCrash.Margin = new Padding(4, 4, 4, 4);
            btnCrash.Name = "btnCrash";
            btnCrash.Size = new Size(118, 36);
            btnCrash.TabIndex = 4;
            btnCrash.Text = "Crash";
            btnCrash.UseVisualStyleBackColor = true;
            btnCrash.Click += btnCrash_Click;
            // 
            // btnViewMessage
            // 
            btnViewMessage.Location = new Point(181, 109);
            btnViewMessage.Margin = new Padding(4, 4, 4, 4);
            btnViewMessage.Name = "btnViewMessage";
            btnViewMessage.Size = new Size(154, 36);
            btnViewMessage.TabIndex = 5;
            btnViewMessage.Text = "View Message";
            btnViewMessage.UseVisualStyleBackColor = true;
            btnViewMessage.Click += btnViewMessage_Click;
            // 
            // btnResume
            // 
            btnResume.Location = new Point(500, 109);
            btnResume.Margin = new Padding(4, 4, 4, 4);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(118, 36);
            btnResume.TabIndex = 6;
            btnResume.Text = "Resume";
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            // 
            // MachineTile
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnResume);
            Controls.Add(btnViewMessage);
            Controls.Add(btnCrash);
            Controls.Add(btnInterrupt);
            Controls.Add(btnSendMessage);
            Controls.Add(lblTime);
            Controls.Add(lblMachineID);
            Margin = new Padding(4, 4, 4, 4);
            Name = "MachineTile";
            Size = new Size(644, 168);
            ResumeLayout(false);
        }

        #endregion

        private Label lblMachineID;
        private Label lblTime;
        private Button btnSendMessage;
        private Button btnInterrupt;
        private Button btnCrash;
        private Button btnViewMessage;
        private Button btnResume;
    }
}
