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


            // Game Loop
            for (int i = 0; i < selectedSentences.Count; i++)
            {
                string testSentence = selectedSentences[i].Trim();
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

            Console.WriteLine($"\nAll rounds complete! Good job. Total Time passed of all {rounds} Rounds: {totalTimePassed:F2}s with {totalErrorCount} Errors.");
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
