using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Models
{
    public class Score
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public int TotalScore { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public DateTime DatePlayed { get; set; } = DateTime.Now;
    }
}
