namespace RaftDashboard
{
    // Machine log entries
    public class LogEntry
    {
        public int Term { get; set; }
        public int Index { get; set; }
        public string Command { get; set; } = "";
    }
}
