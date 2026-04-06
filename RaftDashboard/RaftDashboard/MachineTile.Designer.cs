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
            lblUptime = new Label();
            btnSendMessage = new Button();
            btnInterrupt = new Button();
            btnCrash = new Button();
            btnResume = new Button();
            lblMessage = new Label();
            lblLogIndex = new Label();
            lblSharedStateX = new Label();
            SuspendLayout();
            // 
            // lblMachineID
            // 
            lblMachineID.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMachineID.Location = new Point(25, 27);
            lblMachineID.Margin = new Padding(4, 0, 4, 0);
            lblMachineID.Name = "lblMachineID";
            lblMachineID.Size = new Size(212, 31);
            lblMachineID.TabIndex = 0;
            lblMachineID.Text = "Machine";
            // 
            // lblUptime
            // 
            lblUptime.Location = new Point(25, 58);
            lblUptime.Margin = new Padding(4, 0, 4, 0);
            lblUptime.Name = "lblUptime";
            lblUptime.Size = new Size(439, 31);
            lblUptime.TabIndex = 1;
            lblUptime.Text = "Uptime: ";
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(472, 154);
            btnSendMessage.Margin = new Padding(4);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(146, 36);
            btnSendMessage.TabIndex = 2;
            btnSendMessage.Text = "Send Message";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // btnInterrupt
            // 
            btnInterrupt.Location = new Point(472, 22);
            btnInterrupt.Margin = new Padding(4);
            btnInterrupt.Name = "btnInterrupt";
            btnInterrupt.Size = new Size(146, 36);
            btnInterrupt.TabIndex = 3;
            btnInterrupt.Text = "Interrupt";
            btnInterrupt.UseVisualStyleBackColor = true;
            btnInterrupt.Click += btnInterrupt_Click;
            // 
            // btnCrash
            // 
            btnCrash.Location = new Point(472, 66);
            btnCrash.Margin = new Padding(4);
            btnCrash.Name = "btnCrash";
            btnCrash.Size = new Size(146, 36);
            btnCrash.TabIndex = 4;
            btnCrash.Text = "Crash";
            btnCrash.UseVisualStyleBackColor = true;
            btnCrash.Click += btnCrash_Click;
            // 
            // btnResume
            // 
            btnResume.Location = new Point(472, 110);
            btnResume.Margin = new Padding(4);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(146, 36);
            btnResume.TabIndex = 6;
            btnResume.Text = "Resume";
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(25, 89);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(439, 31);
            lblMessage.TabIndex = 7;
            lblMessage.Text = "Message: ";
            // 
            // lblLogIndex
            // 
            lblLogIndex.Location = new Point(25, 120);
            lblLogIndex.Margin = new Padding(4, 0, 4, 0);
            lblLogIndex.Name = "lblLogIndex";
            lblLogIndex.Size = new Size(439, 31);
            lblLogIndex.TabIndex = 8;
            lblLogIndex.Text = "Log Index: ";
            // 
            // lblSharedStateX
            // 
            lblSharedStateX.Location = new Point(25, 151);
            lblSharedStateX.Margin = new Padding(4, 0, 4, 0);
            lblSharedStateX.Name = "lblSharedStateX";
            lblSharedStateX.Size = new Size(439, 31);
            lblSharedStateX.TabIndex = 9;
            lblSharedStateX.Text = "Shared State (X): ";
            // 
            // MachineTile
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblSharedStateX);
            Controls.Add(lblLogIndex);
            Controls.Add(lblMessage);
            Controls.Add(btnResume);
            Controls.Add(btnCrash);
            Controls.Add(btnInterrupt);
            Controls.Add(btnSendMessage);
            Controls.Add(lblUptime);
            Controls.Add(lblMachineID);
            Margin = new Padding(4);
            Name = "MachineTile";
            Size = new Size(644, 212);
            ResumeLayout(false);
        }

        #endregion

        private Label lblMachineID;
        private Label lblUptime;
        private Button btnSendMessage;
        private Button btnInterrupt;
        private Button btnCrash;
        private Button btnResume;
        private Label lblMessage;
        private Label lblLogIndex;
        private Label lblSharedStateX;
    }
}
