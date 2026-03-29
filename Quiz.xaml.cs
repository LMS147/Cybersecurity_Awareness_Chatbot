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

namespace form
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private bool answered = false;

        public Quiz()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            questions = GetCyberSecurityQuestions();
            ShowQuestion(currentQuestionIndex);
            ScoreText.Text = $"Score: {score} / {questions.Count}";
        }

        private void ShowQuestion(int index)
        {
            if (index < 0 || index >= questions.Count)
                return;

            var q = questions[index];
            QuestionText.Text = q.Question;
            OptionA.Content = q.OptionA;
            OptionB.Content = q.OptionB;
            OptionC.Content = q.OptionC;
            OptionD.Content = q.OptionD;

            // Clear previous selections and result
            OptionA.IsChecked = OptionB.IsChecked = OptionC.IsChecked = OptionD.IsChecked = false;
            ResultText.Text = "";
            ResultText.Foreground = Brushes.LightGreen;
            answered = false;

            ScoreText.Text = $"Score: {score} / {questions.Count}";
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (answered)
            {
                ResultText.Text = "You've already answered. Click Next.";
                ResultText.Foreground = Brushes.Orange;
                return;
            }

            RadioButton selected = new[] { OptionA, OptionB, OptionC, OptionD }
                .FirstOrDefault(rb => rb.IsChecked == true);

            if (selected == null)
            {
                ResultText.Text = "Please select an answer.";
                ResultText.Foreground = Brushes.Orange;
                return;
            }

            var correct = questions[currentQuestionIndex].CorrectOption;
            string selectedOption = selected.Name.Replace("Option", "");
            string selectedText = selected.Content.ToString();

            if (selected.Name == "Option" + correct)
            {
                ResultText.Text = "Correct!";
                ResultText.Foreground = Brushes.LightGreen;
                score++;

                // ✅ Log correct answer
                ActivityLogger.Log($"[Quiz] Question {currentQuestionIndex + 1}: Correct answer selected ({selectedOption}) - \"{selectedText}\"");
            }
            else
            {
                ResultText.Text = $"Incorrect. The correct answer is {correct}.";
                ResultText.Foreground = Brushes.Red;

                // ✅ Log incorrect answer
                ActivityLogger.Log($"[Quiz] Question {currentQuestionIndex + 1}: Incorrect answer selected ({selectedOption}) - \"{selectedText}\". Correct answer was {correct}.");
            }

            ScoreText.Text = $"Score: {score} / {questions.Count}";
            answered = true;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (!answered)
            {
                ResultText.Text = "Please submit an answer before moving to next question.";
                ResultText.Foreground = Brushes.Orange;
                return;
            }

            currentQuestionIndex++;

            if (currentQuestionIndex >= questions.Count)
            {
                // Quiz finished
                MessageBox.Show($"Quiz complete! Your final score is {score} out of {questions.Count}.", "Quiz Finished");
                currentQuestionIndex = 0;
                score = 0;
            }

            ShowQuestion(currentQuestionIndex);
        }


        // Data structure for a question
        public class QuizQuestion
        {
            public string Question { get; set; }
            public string OptionA { get; set; }
            public string OptionB { get; set; }
            public string OptionC { get; set; }
            public string OptionD { get; set; }
            public string CorrectOption { get; set; }
        }

        // Returns the 10 quiz questions
        private List<QuizQuestion> GetCyberSecurityQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    OptionA = "A scam to trick users into revealing personal information",
                    OptionB = "A software update",
                    OptionC = "A type of firewall",
                    OptionD = "An encryption method",
                    CorrectOption = "A"
                },
                new QuizQuestion
                {
                    Question = "How can you identify a fake website?",
                    OptionA = "Check the website color",
                    OptionB = "Verify HTTPS and domain spelling",
                    OptionC = "Ignore the certificate",
                    OptionD = "Use incognito mode only",
                    CorrectOption = "B"
                },
                new QuizQuestion
                {
                    Question = "What is ransomware?",
                    OptionA = "An antivirus tool",
                    OptionB = "A network cable",
                    OptionC = "Software that encrypts files and demands payment",
                    OptionD = "A type of VPN",
                    CorrectOption = "C"
                },
                new QuizQuestion
                {
                    Question = "Which of the following is a strong password?",
                    OptionA = "123456",
                    OptionB = "Password1",
                    OptionC = "mydog",
                    OptionD = "Y7u@92&b!Zr",
                    CorrectOption = "D"
                },
                new QuizQuestion
                {
                    Question = "What is the purpose of a VPN?",
                    OptionA = "Speed up internet",
                    OptionB = "Access blocked websites",
                    OptionC = "Encrypt internet traffic and mask IP",
                    OptionD = "Delete browser history",
                    CorrectOption = "C"
                },
                new QuizQuestion
                {
                    Question = "What does HTTPS in a URL mean?",
                    OptionA = "Website is down",
                    OptionB = "Unsafe connection",
                    OptionC = "Encrypted connection using SSL/TLS",
                    OptionD = "It's a scam site",
                    CorrectOption = "C"
                },
                new QuizQuestion
                {
                    Question = "How do you protect your privacy online?",
                    OptionA = "Click all ads",
                    OptionB = "Use weak passwords",
                    OptionC = "Use VPN and enable 2FA",
                    OptionD = "Always share your location",
                    CorrectOption = "C"
                },
                new QuizQuestion
                {
                    Question = "What is multi-factor authentication?",
                    OptionA = "Logging in twice",
                    OptionB = "Using multiple emails",
                    OptionC = "Verification using more than one method (e.g., SMS + password)",
                    OptionD = "Updating your antivirus",
                    CorrectOption = "C"
                },
                new QuizQuestion
                {
                    Question = "How do you secure smart home devices?",
                    OptionA = "Leave default passwords",
                    OptionB = "Disable firmware updates",
                    OptionC = "Connect to public Wi-Fi",
                    OptionD = "Change default passwords and segment networks",
                    CorrectOption = "D"
                },
                new QuizQuestion
                {
                    Question = "What should you do to defend against ransomware?",
                    OptionA = "Pay the attacker",
                    OptionB = "Ignore email attachments",
                    OptionC = "Back up data regularly and disable macros",
                    OptionD = "Only use public Wi-Fi",
                    CorrectOption = "C"
                }
            };
        }
    }
}
