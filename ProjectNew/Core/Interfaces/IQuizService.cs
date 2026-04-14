using ProjectNew.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.Core.Interfaces
{
    public interface IQuizService
    {
        void StartQuiz(DifficultyLevel level);
        void ShowHistory();
        void ShowStatistics();
    }
}
