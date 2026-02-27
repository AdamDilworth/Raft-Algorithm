using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaftDashboard
{
    public class Message
    {
        public int From {  get; set; }
        public int To { get; set; }
        public string Type { get; set; } = "";
        public string Payload { get; set; } = "";
    }
}
