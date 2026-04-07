namespace RaftDashboard
{
    partial class CommandPromptForm
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
            cmbAction = new ComboBox();
            numValue = new NumericUpDown();
            btnSubmit = new Button();
            label1 = new Label();
            label2 = new Label();
            txtVariable = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)numValue).BeginInit();
            SuspendLayout();
            // 
            // cmbAction
            // 
            cmbAction.FormattingEnabled = true;
            cmbAction.Location = new Point(126, 24);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(236, 33);
            cmbAction.TabIndex = 0;
            // 
            // numValue
            // 
            numValue.Location = new Point(126, 63);
            numValue.Name = "numValue";
            numValue.Size = new Size(236, 31);
            numValue.TabIndex = 1;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(28, 159);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(334, 34);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Send Command";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 27);
            label1.Name = "label1";
            label1.Size = new Size(67, 25);
            label1.TabIndex = 3;
            label1.Text = "Action:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 63);
            label2.Name = "label2";
            label2.Size = new Size(58, 25);
            label2.TabIndex = 4;
            label2.Text = "Value:";
            // 
            // txtVariable
            // 
            txtVariable.Location = new Point(126, 100);
            txtVariable.Name = "txtVariable";
            txtVariable.Size = new Size(236, 31);
            txtVariable.TabIndex = 5;
            txtVariable.Text = "X";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 100);
            label3.Name = "label3";
            label3.Size = new Size(78, 25);
            label3.TabIndex = 6;
            label3.Text = "Variable:";
            // 
            // CommandPromptForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 205);
            Controls.Add(label3);
            Controls.Add(txtVariable);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSubmit);
            Controls.Add(numValue);
            Controls.Add(cmbAction);
            Name = "CommandPromptForm";
            Text = "Send Command";
            Load += CommandPromptForm_Load;
            ((System.ComponentModel.ISupportInitialize)numValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbAction;
        private NumericUpDown numValue;
        private Button btnSubmit;
        private Label label1;
        private Label label2;
        private TextBox txtVariable;
        private Label label3;
    }
}