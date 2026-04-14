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
            string filePath = "sentences.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: sentences.txt not found!");
                return;
            }

            // Load sentences and filter out empty lines
            string[] lines = File.ReadAllLines(filePath).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            int totalAvailable = lines.Length;

            Console.WriteLine($"---------  Typing Game ------------");
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
            int totalErrorCount = 0;
            int totalCharacters = 0;


            // Game Loop
            for (int i = 0; i < selectedSentences.Count; i++)
            {
                string testSentence = selectedSentences[i].Trim();
                totalCharacters += testSentence.Length;

                Console.WriteLine($"Round {i + 1} of {rounds}");
                Console.WriteLine($"Type this: \n\n{testSentence}\n");
                Console.Write("Start typing here: ");

                Stopwatch stopwatch = Stopwatch.StartNew();
                string userInput = Console.ReadLine() ?? "";
                stopwatch.Stop();

                int errorCount = CountErrors(testSentence, userInput);
                totalErrorCount += errorCount;
                var roundTimePassed = stopwatch.Elapsed.TotalSeconds;
                totalTimePassed += roundTimePassed;
                Console.WriteLine($"\nFinished! Time: {roundTimePassed:F2}s, Errors: {errorCount}");
                Console.WriteLine("Press any key for next round...");
                Console.ReadKey(true);
                Console.Clear();
            }


            // --- CALCULATIONS ---
            // WPM = (Characters / 5) / (Time in Minutes)
            double totalMinutes = totalTimePassed / 60.0;
            double wpm = (totalCharacters / 5.0) / totalMinutes;

            // Accuracy = % of correct characters
            double accuracy = Math.Max(0, ((double)(totalCharacters - totalErrorCount) / totalCharacters) * 100);

            Console.WriteLine("===========================================");
            Console.WriteLine("             GAME COMPLETE!                ");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Total Sentences : {rounds}");
            Console.WriteLine($"Total Time      : {totalTimePassed:F2} seconds");
            Console.WriteLine($"Total Errors    : {totalErrorCount}");
            Console.WriteLine($"Average WPM     : {wpm:F1} WPM");
            Console.WriteLine($"Accuracy        : {accuracy:F1}%");
            Console.WriteLine("===========================================");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static int CountErrors(string original, string typed)
        {
            int errors = 0;
            int minLength = Math.Min(original.Length, typed.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (original[i] != typed[i])
                    errors++;
            }
            errors += Math.Abs(original.Length - typed.Length);

            return errors;
        }
    }
}
