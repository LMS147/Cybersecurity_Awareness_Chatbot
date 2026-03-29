using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    public class UserProfile
    {
        public string Name { get; set; } = "Friend";
        public HashSet<string> Interests { get; } = new HashSet<string>();
        public string LastTopic { get; set; } = "";
    }

    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Urgent,
        Positive
    }

    public class ConversationContext
    {
        public string CurrentTopic { get; set; } = "";
        public ThreatLevel ThreatAssessment { get; set; } = ThreatLevel.Normal;
        public Stack<string> TopicHistory { get; } = new Stack<string>();
        public bool RequiresFollowUp { get; set; }
    }

    public enum ThreatLevel
    {
        Normal,
        Elevated,
        Critical
    }

    public class ChatbotEngine
    {
        private static readonly Dictionary<string, string> ExactResponses = new()
        {
            { "how are you?", "As an AI-powered program, I don't experience emotions, but I'm fully operational and ready to assist with cybersecurity matters." },
            { "what is your purpose?", "My primary function is to educate users about cybersecurity best practices and threat prevention strategies." },
            { "what topics can i ask about?", "Ask about: password security, phishing scams, malware prevention, privacy protection, or safe browsing." },
            { "how secure is public wifi?", "Public Wi-Fi is risky - use a VPN, avoid sensitive transactions, and enable firewall protection." },
            { "what's zero-day vulnerability?", "A zero-day is an unknown exploit circulating before developers can create a patch." },
            { "how do vpns work?", "VPNs encrypt your internet traffic and mask your IP address to protect online privacy." },
            { "what is phishing?", "Fraudulent attempts to obtain sensitive information by disguising as trustworthy entities." },
            { "how to detect fake websites?", "Check for HTTPS, domain spelling, and legitimate certificates. Use website reputation tools." },
            { "what is ransomware?", "Malicious software that encrypts files until payment is made. Regular backups are crucial." },
            { "how to secure smart home devices?", "Change default passwords, firmware updates, network segmentation, disable unused features." },
            { "what is multi-factor authentication?", "Security system requiring multiple verification methods (something you know, have, or are)." }
        };

        private static readonly Dictionary<string, string> KeywordResponses = new()
        {
            { "password", "Strong passwords should have 12+ characters with mixed cases, numbers, and symbols. Consider using a password manager!" },
            { "scam", "Verify sender identities before responding to messages. Legitimate organizations won't ask for sensitive data via email." },
            { "privacy", "Use VPNs on public Wi-Fi, review app permissions regularly, and enable two-factor authentication where possible." },
            { "malware", "Install reputable antivirus, avoid suspicious downloads, and regularly scan your systems." },
            { "phishing", "Never click unexpected links. Check email headers and report suspicious messages." },
            { "firewall", "Configure to block unnecessary inbound/outbound connections and update rules regularly." },
            { "ransomware", "Maintain offline backups, disable macro scripts, and educate staff about attachments." },
            { "vpn", "Choose no-logs providers with strong encryption (OpenVPN/IKEv2) and kill switch features." },
            { "social engineering", "Verify identities through secondary channels before sharing sensitive information." },
            { "update", "Enable automatic security patches for OS and applications to fix vulnerabilities." },
            { "backup", "Follow 3-2-1 rule: 3 copies, 2 media types, 1 offsite. Test restores regularly." },
            { "https", "Ensures encrypted connection. Look for padlock icon and valid certificate details." },
            { "iot", "Change default credentials, segment network access, and disable UPnP when unnecessary." }
        };

        private static readonly Dictionary<string, Sentiment> SentimentKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            { "worried", Sentiment.Worried }, { "nervous", Sentiment.Worried },
            { "curious", Sentiment.Curious }, { "confused", Sentiment.Curious },
            { "frustrated", Sentiment.Frustrated }, { "annoyed", Sentiment.Frustrated },
            { "terrified", Sentiment.Worried }, { "panicked", Sentiment.Worried },
            { "violated", Sentiment.Worried }, { "paranoid", Sentiment.Worried },
            { "doubtful", Sentiment.Curious }, { "unsure", Sentiment.Curious },
            { "skeptical", Sentiment.Curious }, { "hesitant", Sentiment.Curious },
            { "enraged", Sentiment.Frustrated }, { "irked", Sentiment.Frustrated },
            { "helpless", Sentiment.Frustrated }, { "overwhelmed", Sentiment.Frustrated },
            { "urgent", Sentiment.Urgent }, { "emergency", Sentiment.Urgent },
            { "critical", Sentiment.Urgent }, { "immediately", Sentiment.Urgent },
            { "thankful", Sentiment.Positive }, { "grateful", Sentiment.Positive },
            { "confident", Sentiment.Positive }, { "protected", Sentiment.Positive }
        };

        private delegate string ResponseHandler(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment);

        private readonly List<ResponseHandler> ResponseStrategies;

        public ChatbotEngine()
        {
            ResponseStrategies = new List<ResponseHandler>()
            {
                HandleEmergencyTrigger,
                HandleExactMatch,
                HandleKeywordMatch,
                HandleContextContinuation,
                HandleSentimentResponse,
                HandleDefaultResponse
            };
        }

        public string GetResponse(string input, UserProfile profile, ref ConversationContext context)
        {
            Sentiment sentiment = DetectSentiment(input);

            foreach (var handler in ResponseStrategies)
            {
                var result = handler(input, profile, ref context, sentiment);
                if (!string.IsNullOrEmpty(result))
                    return PersonalizeResponse(result, profile);
            }

            return HandleDefaultResponse(input, profile, ref context, sentiment);
        }

        private string HandleEmergencyTrigger(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            var crisisKeywords = new[] { "hacked", "breach", "ransomware" };
            if (crisisKeywords.Any(k => input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                context.ThreatAssessment = ThreatLevel.Critical;
                context.CurrentTopic = "incident_response";
                return "🚨 Immediate action required: Disconnect your device from the internet and contact security support!";
            }
            return null;
        }

        private string HandleExactMatch(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            if (string.IsNullOrWhiteSpace(input)) return "Please ask a cybersecurity-related question.";

            var orderedKeys = ExactResponses.Keys.OrderByDescending(k => k.Length);
            foreach (var key in orderedKeys)
            {
                if (input.Contains(key))
                {
                    context.CurrentTopic = key;
                    context.TopicHistory.Push(key);
                    return ExactResponses[key];
                }
                else if (ComputeSimilarity(input, key) >= 0.8)
                {
                    context.CurrentTopic = key;
                    context.TopicHistory.Push(key);
                    return ExactResponses[key];
                }
            }

            return null;
        }

        private string HandleKeywordMatch(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            foreach (var keyword in KeywordResponses.Keys)
            {
                if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    context.TopicHistory.Push(context.CurrentTopic);
                    context.CurrentTopic = keyword;

                    if (!profile.Interests.Contains(keyword))
                        profile.Interests.Add(keyword);

                    return KeywordResponses[keyword];
                }
            }
            return null;
        }

        private string HandleContextContinuation(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            var continuers = new[] { "more", "explain", "details" };
            if (!string.IsNullOrEmpty(context.CurrentTopic) && continuers.Any(input.Contains))
            {
                if (KeywordResponses.TryGetValue(context.CurrentTopic, out var detail))
                    return $"More about {context.CurrentTopic}: {detail}";
            }
            return null;
        }

        private string HandleSentimentResponse(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            var toneModifiers = new Dictionary<Sentiment, string>
            {
                { Sentiment.Worried, "It's wise to be cautious. " },
                { Sentiment.Curious, "Great question! " },
                { Sentiment.Frustrated, "Let's break this down: " },
                { Sentiment.Urgent, "Immediate action needed: " },
                { Sentiment.Positive, "Great security practice: " }
            };

            string keywordResponse = KeywordResponses.TryGetValue(context.CurrentTopic, out var value) ? value : "";
            return toneModifiers.TryGetValue(sentiment, out var modifier) ? $"{modifier}{keywordResponse}" : null;
        }

        private string HandleDefaultResponse(string input, UserProfile profile, ref ConversationContext context, Sentiment sentiment)
        {
            return "I'm not sure I understand. Could you rephrase or ask about cybersecurity topics?";
        }

        private double ComputeSimilarity(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2)) return 0.0;
            int maxLength = Math.Max(s1.Length, s2.Length);
            int distance = ComputeLevenshteinDistance(s1, s2);
            return 1.0 - (double)distance / maxLength;
        }

        private int ComputeLevenshteinDistance(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1)) return s2?.Length ?? 0;
            if (string.IsNullOrEmpty(s2)) return s1.Length;

            int[,] matrix = new int[s1.Length + 1, s2.Length + 1];
            for (int i = 0; i <= s1.Length; i++) matrix[i, 0] = i;
            for (int j = 0; j <= s2.Length; j++) matrix[0, j] = j;

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[s1.Length, s2.Length];
        }

        public static Sentiment DetectSentiment(string input)
        {
            foreach (var pair in SentimentKeywords)
            {
                if (input.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return pair.Value;
            }
            return Sentiment.Neutral;
        }

        public static string PersonalizeResponse(string response, UserProfile profile)
        {
            return profile.Interests.Count > 0
                ? $"{response}\n(Remember: You're interested in {string.Join(", ", profile.Interests)})"
                : response;
        }
    }
}
