namespace RaftDashboard
{
    partial class TargetPromptForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMachineMsg = new Label();
            btnOkay = new Button();
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // lblMachineMsg
            // 
            lblMachineMsg.AutoSize = true;
            lblMachineMsg.Location = new Point(12, 22);
            lblMachineMsg.Name = "lblMachineMsg";
            lblMachineMsg.Size = new Size(195, 20);
            lblMachineMsg.TabIndex = 0;
            lblMachineMsg.Text = "Pick an machine to message";
            // 
            // btnOkay
            // 
            btnOkay.DialogResult = DialogResult.OK;
            btnOkay.Location = new Point(157, 164);
            btnOkay.Name = "btnOkay";
            btnOkay.Size = new Size(94, 29);
            btnOkay.TabIndex = 1;
            btnOkay.Text = "Okay";
            btnOkay.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(56, 80);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 2;
            // 
            // TargetPromptForm
            // 
            AcceptButton = btnOkay;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(263, 205);
            Controls.Add(numericUpDown1);
            Controls.Add(btnOkay);
            Controls.Add(lblMachineMsg);
            Name = "TargetPromptForm";
            Text = "Message a Machine";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMachineMsg;
        private Button btnOkay;
        private NumericUpDown numericUpDown1;
    }
}