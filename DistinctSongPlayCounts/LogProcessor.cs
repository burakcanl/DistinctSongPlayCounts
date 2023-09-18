using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DistinctSongPlayCounts
{
    public class LogProcessor
    {
        public void ProcessLogs(string inputFilePath, string outputFilePath) // Process log data from the input file and write results to the output file
        {
            // Read log entries from the input file
            List<LogEntry> logEntries = ReadLogEntries(inputFilePath);
            // Calculate distinct song counts per client on August 10, 2016
            Dictionary<string, HashSet<string>> clientSongCounts = CalculateDistinctSongCounts(logEntries);

            // Write the results to the output file
            WriteResults(outputFilePath, clientSongCounts);
        }

        private List<LogEntry> ReadLogEntries(string inputFilePath) // Read log entries from the input CSV file
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            // Define the date format for parsing
            string dateFormat = "dd/MM/yyyy HH:mm:ss";
            // Read all lines from the input file
            string[] lines = File.ReadAllLines(inputFilePath);

            foreach (string line in lines.Skip(1)) // Iterate through the lines (skipping the header)
            {
                string[] columns = line.Split('\t');

                // Parse the date and time from the log entry
                if (DateTime.TryParseExact(columns[3], dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime playDate))
                {
                    // Create a LogEntry object and add it to the list
                    logEntries.Add(new LogEntry
                    {
                        ClientId = columns[2],
                        SongId = columns[1],
                        PlayDate = playDate
                    });
                }
            }

            return logEntries;
        }
        private Dictionary<string, HashSet<string>> CalculateDistinctSongCounts(List<LogEntry> logEntries)  // Calculate distinct song counts per client on August 10, 2016
        {
            Dictionary<string, HashSet<string>> clientSongCounts = new Dictionary<string, HashSet<string>>();

            // Iterate through the log entries
            foreach (LogEntry entry in logEntries)
            {
                // Check if the log entry date is August 10, 2016
                if (entry.PlayDate.Date == new DateTime(2016, 8, 10))
                {

                    // Initialize a HashSet for each client if it doesn't exist
                    if (!clientSongCounts.ContainsKey(entry.ClientId))
                    {
                        clientSongCounts[entry.ClientId] = new HashSet<string>();
                    }

                    // Add the song to the client's HashSet
                    clientSongCounts[entry.ClientId].Add(entry.SongId);
                }
            }

            return clientSongCounts;
        }

        private void WriteResults(string outputFilePath, Dictionary<string, HashSet<string>> clientSongCounts)// Write the results to the output CSV file
        {
            // Group the results by distinct song counts and order by the count
            var result = clientSongCounts
                .GroupBy(kvp => kvp.Value.Count)
                .OrderBy(group => group.Key);

            // Write the results to the output file
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("DISTINCT_PLAY_COUNT\tCLIENT_COUNT");

                // Iterate through the grouped results and write them to the file
                foreach (var group in result)
                {
                    writer.WriteLine($"{group.Key}\t{group.Count()}");
                }
            }
        }
    }
}