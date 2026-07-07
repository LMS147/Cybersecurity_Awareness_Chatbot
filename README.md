# Cybersecurity Awareness Chatbot

A comprehensive WPF-based desktop application designed to educate users about cybersecurity best practices through interactive conversations, quizzes, and practical tasks.

## 🎯 Features

### 💬 **Interactive Chatbot**
- **Intelligent Conversation Engine**: Uses natural language processing with Levenshtein distance algorithm for fuzzy matching
- **Sentiment Detection**: Analyzes user emotions (worried, curious, frustrated, urgent, positive) to provide contextually appropriate responses
- **Multi-Strategy Response System**:
  - Emergency detection (hacked, breach, ransomware alerts)
  - Exact phrase matching with high accuracy
  - Keyword-based responses
  - Context continuation for follow-up questions
  - Sentiment-based tone modulation
  - Personalized responses based on user interests

- **Audio Greeting**: Welcome sequence with ASCII art and voice greeting
- **Chat History**: Session management with persistent message history

### 📚 **Educational Content**
The chatbot covers critical cybersecurity topics including:
- Password Security & Best Practices
- Phishing Detection & Prevention
- Malware & Ransomware Protection
- VPN & Encryption
- Multi-Factor Authentication (MFA)
- Safe Browsing & HTTPS
- Social Engineering Awareness
- Privacy Protection
- IoT Device Security
- Regular Updates & Patching
- Backup Strategies (3-2-1 Rule)
- Firewall Configuration

### 🧩 **Additional Features**
- **Quiz Module**: Interactive quizzes to test cybersecurity knowledge
- **Task Manager**: Practical cybersecurity tasks and exercises
- **Activity Logger**: Track all user interactions and learning activity
- **User Profiles**: Personalized experience based on user interests and learning history

## 🏗️ Project Structure

```
Cybersecurity_Awareness_Chatbot/
├── ChatbotEngine.cs           # Core NLP and response logic
├── ChatPage.xaml/.cs          # Main chat interface
├── MainWindow.xaml/.cs        # Application main window
├── Quiz.xaml/.cs              # Quiz module
├── TaskPage.xaml/.cs          # Tasks module
├── ActivityPage.xaml/.cs      # Activity logging page
├── ActivityLogger.cs          # Activity tracking
├── greeting.wav               # Audio greeting file
└── [WPF Project Files]        # XAML markup and configuration
```

## 🔧 Core Components

### **ChatbotEngine.cs**
The intelligent heart of the application featuring:
- **UserProfile**: Tracks user name and interests
- **ConversationContext**: Maintains conversation state and topic history
- **Sentiment Enum**: Identifies emotional context (Neutral, Worried, Curious, Frustrated, Urgent, Positive)
- **ThreatLevel**: Assesses cybersecurity threat severity
- **Response Handlers**: Multiple strategies for generating contextually appropriate responses
- **Similarity Algorithms**: Levenshtein distance for robust pattern matching

### **ChatPage.xaml.cs**
Interactive chat interface with:
- Real-time message bubbles (user in blue, AI in gray)
- Typing animation for bot responses
- Session management
- Audio playback support
- Keyboard input handling (Enter to send)

### **MainWindow.xaml.cs**
Application shell featuring:
- Navigation between chat, tasks, quiz, and activity pages
- Chat session management
- Central hub for all features

## 🚀 Getting Started

### Prerequisites
- **.NET Framework 4.7.2+** or **.NET 5.0+**
- **Visual Studio 2019+** or compatible IDE
- Windows 10 or later

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/LMS147/Cybersecurity_Awareness_Chatbot.git
   cd Cybersecurity_Awareness_Chatbot
   ```

2. **Open in Visual Studio**
   - Open `Cybersecurity_Awareness_Chatbot.sln`
   - Restore NuGet packages if needed

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```
   Or run directly from Visual Studio (F5)

## 💡 Usage

### Chat Interaction
1. Launch the application
2. Type your cybersecurity questions in the input field
3. Press Enter or click Send
4. Receive AI-powered responses with personalized recommendations

### Example Queries
- "How secure is public WiFi?"
- "What is phishing?"
- "How do I create a strong password?"
- "I'm worried about ransomware, help!"
- "Tell me more about VPNs"

### Emergency Detection
The bot automatically escalates responses for critical keywords:
- **"hacked"** → Immediate action protocols
- **"breach"** → Security incident guidance
- **"ransomware"** → Critical threat response

### Navigation
- **New Chat**: Start a fresh conversation session
- **Tasks**: Access practical cybersecurity exercises
- **Quiz**: Test your knowledge with interactive assessments
- **Activity**: View your learning history and progress

## 🧠 How the Chatbot Works

### Response Strategy Stack
The engine uses a prioritized handler chain:

1. **Emergency Trigger Handler**
   - Detects critical keywords (hacked, breach, ransomware)
   - Sets threat level to Critical
   - Returns urgent action guidance

2. **Exact Match Handler**
   - Finds precise question matches
   - Uses 80%+ similarity threshold for fuzzy matching

3. **Keyword Match Handler**
   - Identifies relevant keywords in user input
   - Tracks user interests

4. **Context Continuation Handler**
   - Responds to follow-up requests ("more", "explain", "details")
   - Uses conversation history

5. **Sentiment Handler**
   - Modifies response tone based on detected emotions
   - Personalizes communication style

6. **Default Handler**
   - Graceful fallback for unrecognized queries

### Sentiment Detection
The engine recognizes emotional keywords:
- **Worried**: terrified, panicked, violated, paranoid
- **Curious**: confused, doubtful, unsure, skeptical
- **Frustrated**: annoyed, enraged, helpless, overwhelmed
- **Urgent**: emergency, critical, immediately
- **Positive**: thankful, grateful, confident, protected

## 📋 Knowledge Base

The chatbot includes comprehensive responses for:
- 11 exact match patterns for common questions
- 13 keyword-based response templates
- 14+ sentiment keywords for emotional context

Topics are dynamically expandable through `ExactResponses` and `KeywordResponses` dictionaries.

## 🛡️ Security Features

- **Secure Design Patterns**: Best practice recommendations
- **No External API Calls**: Fully self-contained
- **Local Data Storage**: All chat history stored locally
- **Activity Logging**: Track and audit user interactions

## 🧪 Testing

The application includes:
- Activity logging for verification
- Session history persistence
- Integration testing through interactive quizzes

## 📝 License

This project is licensed under the **MIT License** - see [LICENSE.txt](LICENSE.txt) for details.

## 👤 Author

**LMS147**
- GitHub: [@LMS147](https://github.com/LMS147)

## 🤝 Contributing

Contributions are welcome! Please feel free to:
1. Report bugs via GitHub Issues
2. Suggest new features
3. Submit pull requests with improvements
4. Expand the knowledge base with new topics

## 🔮 Future Enhancements

- [ ] Database backend for persistent user profiles
- [ ] Multi-language support
- [ ] Advanced NLP using ML models
- [ ] Web-based version
- [ ] Mobile application
- [ ] Integration with security tools
- [ ] Gamification and achievement system
- [ ] Real-time threat intelligence feeds

## ❓ FAQ

**Q: Can I use this for enterprise training?**
A: Yes! The application is designed for educational use and can be adapted for organizational cybersecurity awareness programs.

**Q: Does it require internet?**
A: No, the application runs completely offline with no external dependencies.

**Q: Can I add new topics?**
A: Absolutely! Modify the `ExactResponses` and `KeywordResponses` dictionaries in `ChatbotEngine.cs`.

**Q: Is there a web version?**
A: Currently, this is a desktop WPF application. Web version planned for future releases.

---

**Stay Secure! 🔐**
