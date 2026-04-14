using System.Text.Json;

namespace TypingGame
{
    public class LeaderboardManager
    {
        private const string LeaderboardFile = "leaderboard.json";

        public void SaveScore(ScoreEntry score)
        {
            var leaderboard = LoadLeaderboard();
            leaderboard.Add(score);
            
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(leaderboard, options);
            File.WriteAllText(LeaderboardFile, json);
        }

        public List<ScoreEntry> LoadLeaderboard()
        {
            if (!File.Exists(LeaderboardFile))
            {
                return new List<ScoreEntry>();
            }

            try
            {
                string json = File.ReadAllText(LeaderboardFile);
                return JsonSerializer.Deserialize<List<ScoreEntry>>(json) ?? new List<ScoreEntry>();
            }
            catch
            {
                return new List<ScoreEntry>();
            }
        }

        public void DisplayLeaderboard(int topCount = 10)
        {
            var leaderboard = LoadLeaderboard();

            if (leaderboard.Count == 0)
            {
                Console.WriteLine("No scores yet! Play a game to get on the leaderboard.");
                return;
            }

            // Define column widths for better formatting
            const int RANK_STYLING = -6;
            const int WPM_STYLING = -8;
            const int ACCURACY_STYLING = -12;
            const int SENTENCES_STYLING = -12;
            const int MISTAKES_STYLING = -10;
            const int DATE_STYLING = -18;

            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine("           TOP " + Math.Min(topCount, leaderboard.Count) + " LEADERBOARD");
            Console.WriteLine("=======================================================================");
            Console.WriteLine($"{"Rank", RANK_STYLING} {"WPM", WPM_STYLING} {"Accuracy",ACCURACY_STYLING} {"Sentences",SENTENCES_STYLING} {"Mistakes",MISTAKES_STYLING} {"Date",DATE_STYLING}");
            Console.WriteLine("-----------------------------------------------------------------------");

            var topScores = leaderboard
                .OrderByDescending(s => s.WPM)
                .Take(topCount)
                .ToList();

            for (int i = 0; i < topScores.Count; i++)
            {
                var score = topScores[i];
                Console.WriteLine($"{i + 1, RANK_STYLING} {score.WPM,WPM_STYLING:F1} {score.Accuracy,ACCURACY_STYLING:F1}% {score.Rounds,SENTENCES_STYLING} {score.Mistakes,MISTAKES_STYLING} {score.DatePlayed,DATE_STYLING:yyyy-MM-dd HH:mm}");
            }

            Console.WriteLine("=======================================================================");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
