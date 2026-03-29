using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace form
{
    /// <summary>
    /// Interaction logic for ActivityPage.xaml
    /// </summary>
    public partial class ActivityPage : Page
    {
        private readonly string logFilePath = "activity_log.txt";

        public ActivityPage()
        {
            InitializeComponent();
            LoadActivityLog();
        }

        /// <summary>
        /// Data model for activity log entry.
        /// </summary>
        public class LogEntry
        {
            public string Date { get; set; }
            public string Time { get; set; }
            public string ActivityType { get; set; }
            public string Message { get; set; }
        }

        private void LoadActivityLog()
        {
            var entries = new List<LogEntry>();

            try
            {
                if (!File.Exists(logFilePath))
                {
                    entries.Add(new LogEntry { Date = "-", Time = "-", ActivityType = "-", Message = "No activity log found." });
                    ActivityLogList.ItemsSource = entries;
                    return;
                }

                var lines = File.ReadAllLines(logFilePath);
                if (lines.Length == 0)
                {
                    entries.Add(new LogEntry { Date = "-", Time = "-", ActivityType = "-", Message = "Activity log is empty." });
                    ActivityLogList.ItemsSource = entries;
                    return;
                }

                foreach (var line in lines)
                {
                    // Expected format: date|time|activityType|message
                    var parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        entries.Add(new LogEntry
                        {
                            Date = parts[0],
                            Time = parts[1],
                            ActivityType = parts[2],
                            Message = parts[3]
                        });
                    }
                    else
                    {
                        entries.Add(new LogEntry
                        {
                            Date = "-",
                            Time = "-",
                            ActivityType = "-",
                            Message = line
                        });
                    }
                }

                ActivityLogList.ItemsSource = entries;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading activity log: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
