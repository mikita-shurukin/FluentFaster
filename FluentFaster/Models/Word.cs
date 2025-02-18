namespace FluentFaster.Models
{
    public class Word
    {
        public int WordID { get; set; } 
        public string Text { get; set; } 
        public int GameID { get; set; }

        public Game Game { get; set; } 
    }
}
