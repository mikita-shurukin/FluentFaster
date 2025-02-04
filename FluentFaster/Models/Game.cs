namespace FluentFaster.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public List<GrammarTopic> GrammarTopics { get; set; }
    }
}
