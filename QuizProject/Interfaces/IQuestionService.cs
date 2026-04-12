using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Interfaces
{
    public interface IQuestionService
    {
        void AddQuestion(MultipleChoiceQuestion question);

        List<MultipleChoiceQuestion> GetAllQuestions();

        void DeleteQuestion(Guid id);
    }
}
