namespace DiabetWebSite.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int Points { get; set; }
    }

    public class UserAnswer
    {
        public int Id { get; set; } // Birincil anahtar
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}