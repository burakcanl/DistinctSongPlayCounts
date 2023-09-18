using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistinctSongPlayCounts
{
    public class LogEntry // Represents a log entry with play ID, client ID, song ID, and play date
    {
        public string PlayId { get; set; }
        public string ClientId { get; set; }
        public string SongId { get; set; }
        public DateTime PlayDate { get; set; }
    }
}
