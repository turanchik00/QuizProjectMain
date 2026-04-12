using QuizProject.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }

        public int CorrectOptionIndex { get; set; }

        public MultipleChoiceQuestion(string text, Difficulty difficultyLevel, List<string> options, int correctOptionIndex) : base (text, difficultyLevel)
        {
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
        }
    }
}
