using ProjectNew.Core.Enums;
using ProjectNew.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.Core.Interfaces
{
    public interface IQuestionService
    {
        void AddQuestion();
        void ViewQuestions();
        void DeleteQuestion();
        List<Question> GetAllQuestions();
    }
}
