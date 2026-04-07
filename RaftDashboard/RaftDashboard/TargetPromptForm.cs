namespace RaftDashboard
{
    public partial class TargetPromptForm : Form
    {
        public TargetPromptForm()
        {
            InitializeComponent();
        }

        public int TargetID => (int)numericUpDown1.Value;
    }
}
