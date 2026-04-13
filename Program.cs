using System.Diagnostics;

namespace TypingGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // The Text the User has to Write down as fast as possible
            string testSentence = "The Programmer is getting ready to do his Job!";

            Console.WriteLine($"---------  Typing Game ------------");
            Console.WriteLine("Type the following sentence as fast as you can");
            Console.WriteLine($"\n {testSentence}\n");

            //Wait for the user to start typing
            Console.WriteLine("Press any key to start the Typing Game...");
            Console.ReadKey();

            //Clear the screen, show the sentence, prompt user
            Console.Clear();
            Console.WriteLine($"{testSentence}\n");
            Console.Write("Start typing here: ");

            //Start a timer and capture the user's input
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string userInput = Console.ReadLine() ?? "";

            //Stop the timer when the user finishes typing
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;

            //Count mistakes
            int errorCount = CountErrors(testSentence, userInput);

            //Show the time taken and the errors
            Console.WriteLine("\n--- Results ---");
            Console.WriteLine($"Time Taken: {timeTaken.TotalSeconds:F2} seconds");
            Console.WriteLine($"Errors: {errorCount}");
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
