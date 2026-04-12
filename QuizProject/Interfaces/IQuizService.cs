using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Interfaces
{
    public interface IQuizService
    {
        void StartQuiz();

        void ShowResult(int correctAnswers, int totalQuestions);
    }
}
