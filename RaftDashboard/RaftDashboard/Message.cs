using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RaftDashboard
{
    // Machine messages
    public class Message
    {
        public int From { get; set; }
        public int To { get; set; }
        public string Type { get; set; } = "";
        // Use string for JSON so it breaks at compile time rather than runtime
        public string PayloadJson { get; set; } = "";
    }
}
