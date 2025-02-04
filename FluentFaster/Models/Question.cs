namespace FluentFaster.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int TestID { get; set; }
        public Test Test { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
    }
}
