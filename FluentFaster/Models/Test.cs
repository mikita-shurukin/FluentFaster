namespace FluentFaster.Models
{
    public class Test
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public int TopicID { get; set; }
        public GrammarTopic GrammarTopic { get; set; }
        public List<Question> Questions { get; set; }
    }
}
