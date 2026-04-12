using QuizProject.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Models
{
    public abstract class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public Difficulty DifficultyLevel { get; set; }

        protected Question(string text, Difficulty difficultyLevel) 
        { 
            Text = text;
            DifficultyLevel = difficultyLevel;
        }
    }
}
