# 🛡️ Cybersecurity_Awareness_Chatbot
 

Welcome to the Cybersecurity Awareness Bot , your virtual assistant for learning about digital safety and best practices! This bot helps users understand cybersecurity concepts, provides tips on staying safe online, and answers common questions related to password security, phishing, malware prevention, and more.

🌟 Features
    -Interactive Chatbot : Engage in a conversational experience with the bot to ask cybersecurity-related questions.
    -Personalized Greetings : The bot greets users by name and creates a friendly atmosphere.
    -Dynamic Responses : Predefined responses cover a wide range of cybersecurity topics.
    -Audio and Visual Enhancements :
    -Plays a voice greeting (greeting.wav) upon initialization.
    -Displays visually appealing ASCII art with a slow reveal effect.
    -Error Handling : Gracefully handles invalid inputs and missing files.
    -Logging : Logs user interactions for debugging and analysis.

⚙️ Prerequisites
    Before running the bot, ensure you have the following installed:

  .NET SDK 6.0 or higher
      A compatible audio file (greeting.wav) for the voice greeting (optional but recommended).

🚀 Installation

  1. Clone the Repository :
           git clone https://github.com/your-username/your-repository-name.git
           cd your-repository-name
   2.Restore Dependencies :
      Open the project in Visual Studio or run the following command:
         dotnet restore
   3.Prepare Audio File :
     Place a greeting.wav file in the root directory of the project. If you don't have one, the bot will notify you at runtime.

🖥️ Usage
1.Run the Bot :
  Use Visual Studio to start the project, or run the following command:
      dotnet run
2.Interact with the Bot :
    The bot will greet you and ask for your name.
    Type your questions about cybersecurity (e.g., "how can I create a strong password?").
    Type exit to terminate the session.
3.View Logs :
    User interactions are logged in the chatbot_log.txt file for debugging and analysis.

🔧 Continuous Integration (CI)
This project uses GitHub Actions for Continuous Integration (CI). The CI pipeline ensures that:

  -The code builds successfully.
  -Syntax errors and formatting issues are detected.
  -Unit tests (if added) pass.
Workflow Details
  -Trigger : The workflow runs on every push or pull_request to the main branch.
  -Steps :
    1.Check out the code.
    2.Set up the .NET SDK.
    3.Restore dependencies.
    4.Build the project.
    5.Run tests (if applicable).
You can view the workflow status in the Actions tab of this repository.

🤝 Contributing
  We welcome contributions to improve the bot! Here's how you can contribute:

  1.Fork the Repository :
    Click the "Fork" button to create a copy of this repository under your account.
  2.Create a Branch:
    git checkout -b feature/your-feature-name
  3.Make Changes :
    Add new features, fix bugs, or improve documentation.
  4.Submit a Pull Request :
    Push your changes to your fork and open a pull request to the main branch of this repository.
