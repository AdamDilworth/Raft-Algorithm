namespace RaftDashboard
{
    public class PersistentState
    {
        public int CurrentTerm { get; set; }
        public int? VotedFor { get; set; }
        public List<LogEntry> Log { get; set; } = new();
    }
}
