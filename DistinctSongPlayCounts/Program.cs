using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DistinctSongPlayCounts;

class Program
{
    static void Main()
    {
        // Specify the input and output file paths
        string inputFilePath = "C:/Users/CAN/source/repos/DistinctSongPlayCounts/DistinctSongPlayCounts/exhibitA-input.csv"; // CSV dosyasının adını ve yolunu belirtin
        string outputFilePath = "C:/Users/CAN/source/repos/DistinctSongPlayCounts/DistinctSongPlayCounts/output.csv";

        // Create an instance of the LogProcessor class
        LogProcessor processor = new LogProcessor();

        // Process the log data and write the results to the output file
        processor.ProcessLogs(inputFilePath, outputFilePath);
    }
}
