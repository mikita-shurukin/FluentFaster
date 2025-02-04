namespace FluentFaster.Models
{
    public class AnswerOption
    {
        public int OptionID { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
    }
}
