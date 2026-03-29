using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace form
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ChatSession> chatSessions = new List<ChatSession>();

        public MainWindow()
        {
            InitializeComponent();

            // Wire up selection changed event on the ListBox
            ChatHistory.SelectionChanged += ChatHistory_SelectionChanged;

            // Start with a new chat session
            StartNewChat();
        }

        private void StartNewChat()
        {
            var session = new ChatSession
            {
                Title = $"Chat {DateTime.Now:HH:mm:ss}",
                Messages = new List<string>()
            };

            chatSessions.Add(session);
            ChatHistory.Items.Add(session);

            MainFrame.Navigate(new ChatPage());
        }

        private void NewChat_Click(object sender, RoutedEventArgs e)
        {
            StartNewChat();
        }

        private void OpenTask_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TaskPage());
        }

        private void OpenQuiz_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Quiz());
        }

        private void OpenActivity_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ActivityPage());
        }

        private void ChatHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChatHistory.SelectedItem is ChatSession session)
            {
                MainFrame.Navigate(new ChatPage());
            }
        }
    }

    public class ChatSession
    {
        public string Title { get; set; }
        public List<string> Messages { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}