namespace FluentFaster.Models
{
    public class GrammarTopic
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public int GameID { get; set; }
        public Game Game { get; set; }
        public List<Test> Tests { get; set; }

    }
}
