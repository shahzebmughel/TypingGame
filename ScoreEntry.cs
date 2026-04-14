namespace TypingGame
{
    public class ScoreEntry
    {
        public DateTime DatePlayed { get; set; }
        public int Rounds { get; set; }
        public double TimeSeconds { get; set; }
        public int Mistakes { get; set; }
        public double WPM { get; set; }
        public double Accuracy { get; set; }
    }
}
