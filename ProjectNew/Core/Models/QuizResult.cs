using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.Core.Models
{
    public class QuizResult
    {
        public string Username { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public string Difficulty { get; set; }
        public DateTime Date { get; set; }
    }
}
