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
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        private readonly List<string> tasks = new List<string>();
        private readonly string taskFilePath = "tasks.txt";

        public TaskPage()
        {
            InitializeComponent();
            TaskInput.KeyDown += TaskInput_KeyDown;
            LoadTasks();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void TaskInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
            }
        }

        private void AddTask()
        {
            string taskText = TaskInput.Text.Trim();

            if (!string.IsNullOrEmpty(taskText))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd | HH:mm:ss");
                string fullTask = $"[{timestamp}] {taskText}";

                tasks.Add(fullTask);
                TaskList.Items.Add(fullTask);
                ActivityLogger.Log("[Task] New task added: " + taskText);

                TaskInput.Clear();
                SaveTasks();
            }
            else
            {
                MessageBox.Show("Please enter a task.", "Empty Task", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadTasks()
        {
            if (File.Exists(taskFilePath))
            {
                var loadedTasks = File.ReadAllLines(taskFilePath);
                tasks.AddRange(loadedTasks);
                foreach (var task in loadedTasks)
                {
                    TaskList.Items.Add(task);
                }
            }
        }

        private void SaveTasks()
        {
            File.WriteAllLines(taskFilePath, tasks);
        }
    }
}
