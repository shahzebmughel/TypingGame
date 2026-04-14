using System.Diagnostics;
using System.IO; 
using System.Collections.Generic;
using System.Linq;

namespace TypingGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var leaderboardManager = new LeaderboardManager();
            bool playGame = true;

            while (playGame)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("        TYPING GAME - MAIN MENU        ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. View Leaderboard");
                Console.WriteLine("3. Exit");
                Console.WriteLine("========================================");
                Console.Write("Select an option (1-3): ");

                string? menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        PlayGame(leaderboardManager);
                        break;
                    case "2":
                        leaderboardManager.DisplayLeaderboard();
                        break;
                    case "3":
                        playGame = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void PlayGame(LeaderboardManager leaderboardManager)
        {
            string filePath = "sentences.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: sentences.txt not found!");
                return;
            }

            // Load sentences and filter out empty lines
            string[] lines = File.ReadAllLines(filePath).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            int totalAvailable = lines.Length;

            Console.Clear();
            Console.WriteLine($"---------  Typing Game (Live Feedback) ------------");
            Console.WriteLine($"There are {totalAvailable} sentences available.");


            // Ask user for number of rounds
            int rounds = 0;
            while (rounds < 1 || rounds > totalAvailable)
            {
                Console.Write($"How many sentences would you like to type (1-{totalAvailable})? ");
                string input = Console.ReadLine() ?? "";
                int.TryParse(input, out rounds);
            }
            Console.Clear();

            // Shuffle the sentences so they aren't in the same order
            Random rand = new Random();
            var selectedSentences = lines.OrderBy(x => rand.Next()).Take(rounds).ToList();

            double totalTimePassed = 0;
            int totalMistakesMade = 0; 
            int totalCharacters = 0;

            //Gane Loop
            for (int i = 0; i < selectedSentences.Count; i++)
            {
                string target = selectedSentences[i].Trim();
                totalCharacters += target.Length;

                Console.WriteLine($"Round {i + 1} of {rounds}");
                Console.WriteLine("Type the text below:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(target);
                Console.ResetColor();
                Console.WriteLine("--------------------------------------------------");

                // This is where the user starts typing
                string currentInput = "";
                int roundMistakes = 0;
                Stopwatch sw = new Stopwatch();
                bool started = false;

                while (currentInput.Length < target.Length)
                {
                    // Read key without displaying it
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    // Start timer on first key press
                    if (!started)
                    {
                        sw.Start();
                        started = true;
                    }

                    // Handle Backspace
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (currentInput.Length > 0)
                        {
                            currentInput = currentInput.Substring(0, currentInput.Length - 1);
                            // Move cursor back, print space, move cursor back again
                            Console.Write("\b \b");
                        }
                        continue;
                    }

                    // Ignore non-character keys (like Shift, F1, etc.)
                    if (char.IsControl(keyInfo.KeyChar)) continue;

                    char typedChar = keyInfo.KeyChar;
                    char expectedChar = target[currentInput.Length];

                    if (typedChar == expectedChar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        roundMistakes++;
                    }

                    // Display the character and add to input string
                    Console.Write(typedChar);
                    currentInput += typedChar;
                    Console.ResetColor();
                }

                sw.Stop();
                totalTimePassed += sw.Elapsed.TotalSeconds;
                totalMistakesMade += roundMistakes;

                Console.WriteLine("\n\nRound Finished!");
                Console.WriteLine($"Mistakes this round: {roundMistakes}");
                Console.WriteLine("Press any key for next round...");
                Console.ReadKey(true);
                Console.Clear();
            }

            // --- CALCULATIONS ---
            // WPM = (Characters / 5) / (Time in Minutes)
            double totalMinutes = totalTimePassed / 60.0;
            double wpm = (totalCharacters / 5.0) / totalMinutes;

            // Accuracy = % of correct characters
            double accuracy = Math.Max(0, ((double)(totalCharacters - totalMistakesMade) / totalCharacters) * 100);

            Console.WriteLine("===========================================");
            Console.WriteLine("             GAME COMPLETE!                ");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Total Sentences : {rounds}");
            Console.WriteLine($"Total Time      : {totalTimePassed:F2}s");
            Console.WriteLine($"Total Mistakes  : {totalMistakesMade}");
            Console.WriteLine($"Average WPM     : {wpm:F1}");
            Console.WriteLine($"Accuracy        : {accuracy:F1}%");
            Console.WriteLine("===========================================");

            // Save score to leaderboard
            var scoreEntry = new ScoreEntry
            {
                DatePlayed = DateTime.Now,
                Rounds = rounds,
                TimeSeconds = totalTimePassed,
                Mistakes = totalMistakesMade,
                WPM = wpm,
                Accuracy = accuracy
            };

            leaderboardManager.SaveScore(scoreEntry);
            Console.WriteLine("\n✓ Score saved to leaderboard!");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
    }
}
