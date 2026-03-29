using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CyberSecurityBot;
using Microsoft.VisualBasic;

namespace form
{
    public partial class ChatPage : Page
    {
        private readonly UserProfile _userProfile = new UserProfile();
        private ConversationContext _context = new ConversationContext();
        private ChatSession _currentSession;
        private static bool hasBooted = false;
        private ChatbotEngine chatbotEngine = new ChatbotEngine();

        public ChatPage(ChatSession session = null)
        {
            InitializeComponent();
            _currentSession = session ?? new ChatSession() { Title = "New Chat" };

            // Load previous messages into chat panel
            foreach (var msg in _currentSession.Messages)
            {
                AddMessageToChatPanel(msg);
            }

            ShowBootSequence();
            ChatScrollViewer.ScrollToEnd();
        }

        private void ShowBootSequence()
        {
            if (hasBooted) return;
            hasBooted = true;

            // Play audio in background
            Thread audioThread = new Thread(PlayVoiceGreeting);
            audioThread.IsBackground = true;
            audioThread.Start();

            // Display ASCII art directly in the chat
            DisplayASCIIArt();

            // Add welcome message directly to chat
            string welcome = $"Hello! I'm your Cybersecurity Assistant. Ask me anything about digital safety.";
            AddMessageToChatPanel($"AI: {welcome}");
            _currentSession.Messages.Add($"AI: {welcome}");
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(filePath))
                {
                    using (var player = new SoundPlayer(filePath))
                    {
                        player.Play();
                    }
                }
            }
            catch
            {
                // Gracefully ignore audio errors
            }
        }

        private void DisplayASCIIArt()
        {
            string asciiArt = @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗   
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝   
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝    
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝     
╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║      
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝      
";

            var asciiBlock = new TextBlock
            {
                Text = asciiArt,
                FontFamily = new FontFamily("Consolas"),
                Foreground = Brushes.Cyan,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            };

            var border = new Border
            {
                Child = asciiBlock,
                Background = Brushes.Transparent,
                Padding = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            ChatPanel.Children.Add(border);
        }

        /// <summary>
        /// Adds a message bubble to the chat panel.
        /// </summary>
        /// <param name="message">Message text, prefixed with "You:" or "AI:"</param>
        private void AddMessageToChatPanel(string message)
        {
            bool isUser = message.StartsWith("You:");
            var border = new Border
            {
                Background = isUser ? Brushes.DodgerBlue : Brushes.DimGray,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 5, 0, 5),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                MaxWidth = 300,
                Child = new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap
                }
            };
            ChatPanel.Children.Add(border);
            ChatScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// Handles the Send button click event.
        /// </summary>
        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            await HandleUserInputAsync();
        }

        /// <summary>
        /// Handles Enter key in UserInput TextBox.
        /// </summary>
        private async void UserInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                await HandleUserInputAsync();
            }
        }

        /// <summary>
        /// Processes user input: adds user message, gets response, shows bot message.
        /// </summary>
        private async Task HandleUserInputAsync()
        {
            string userText = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText))
                return;

            // Exit condition
            if (userText.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Application.Current.Shutdown(); // 👈 This exits the app
                return;
            }

            // Add user message bubble
            AddMessageToChatPanel("You: " + userText);
            _currentSession.Messages.Add("You: " + userText);

            // Detect sentiment using your ChatbotEngine method
            Sentiment sentiment = ChatbotEngine.DetectSentiment(userText);



            // Get raw bot reply from engine
            string botReply = chatbotEngine.GetResponse(userText.ToLower(), _userProfile, ref _context);

            // Personalize bot reply based on user profile
            string personalizedReply = ChatbotEngine.PersonalizeResponse(botReply, _userProfile);

            // Animate bot typing and show message
            await ShowBotTypingAsync(personalizedReply);

            // Clear user input for next message
            UserInput.Clear();

            // Log activity if you have a logger, otherwise comment out
            ActivityLogger.Log($"User sent message: {userText}");
        }

        /// <summary>
        /// Animates typing effect for bot's response.
        /// </summary>
        /// <param name="message">Bot's reply to display</param>
        private async Task ShowBotTypingAsync(string message)
        {
            var botTextBlock = new TextBlock
            {
                Text = "",
                Foreground = Brushes.WhiteSmoke,
                TextWrapping = TextWrapping.Wrap
            };

            var botBorder = new Border
            {
                Background = Brushes.DimGray,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 5, 0, 15),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300,
                Child = botTextBlock
            };

            ChatPanel.Children.Add(botBorder);
            ChatScrollViewer.ScrollToEnd();

            const int typingDelay = 20; // ms delay per char
            string prefix = "AI: ";

            foreach (char c in prefix + message)
            {
                botTextBlock.Text += c;
                await Task.Delay(typingDelay);
                ChatScrollViewer.ScrollToEnd();
            }

            // Save bot message to session history
            _currentSession.Messages.Add(prefix + message);
        }

        /// <summary>
        /// Chat session to save chat history.
        /// </summary>
        public class ChatSession
        {
            public string Title { get; set; }
            public List<string> Messages { get; set; } = new List<string>();
        }
    }
}
