namespace RaftDashboard
{
    public partial class CommandPromptForm : Form
    {
        public CommandPromptForm()
        {
            InitializeComponent();

            // Populate dropdown
            cmbAction.Items.AddRange(new string[] { "SET", "ADD", "SUBTRACT", "MULTIPLY" });
            cmbAction.SelectedIndex = 0;

            // Button to close the form
            btnSubmit.DialogResult = DialogResult.OK;
            // Allow pressing enter to submit
            this.AcceptButton = btnSubmit;
        }

        public string CommandString
        {
            get
            {
                string varName = string.IsNullOrWhiteSpace(txtVariable.Text) ? "X" : txtVariable.Text.Trim();
                return $"{cmbAction.SelectedItem} {varName} {numValue.Value}";
            }
        }

        private void CommandPromptForm_Load(object sender, EventArgs e)
        {

        }
    }
}
