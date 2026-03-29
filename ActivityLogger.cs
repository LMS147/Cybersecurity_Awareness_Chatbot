using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace form
{
    // Class to log activities such as quiz, tasks, reminders, etc., with persistence
    public static class ActivityLogger
    {
        // Internal list to store log entries in memory
        private static readonly List<string> logs = new List<string>();

        // File path for saving/loading logs
        private static readonly string logFilePath = "activity_log.txt";

        // Provides a readonly view of the activity log entries
        public static IReadOnlyList<string> Logs => logs.AsReadOnly();

        // Adds a new log entry with timestamp and saves to file
        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] {message}";
            logs.Add(logEntry);
            SaveLogsToFile();
        }

        // Clears all entries from the activity log and updates the file
        public static void ClearLogs()
        {
            logs.Clear();
            SaveLogsToFile();
        }

        // Saves the current in-memory logs to the file
        public static void SaveLogsToFile()
        {
            try
            {
                File.WriteAllLines(logFilePath, logs);
            }
            catch (Exception ex)
            {
                // Optionally handle exceptions (e.g. log to debug output)
                Console.WriteLine("Failed to save logs: " + ex.Message);
            }
        }

        // Loads logs from the file into memory
        public static void LoadLogsFromFile()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    logs.Clear();
                    logs.AddRange(File.ReadAllLines(logFilePath));
                }
            }
            catch (Exception ex)
            {
                // Optionally handle exceptions
                Console.WriteLine("Failed to load logs: " + ex.Message);
            }
        }
    }
}
