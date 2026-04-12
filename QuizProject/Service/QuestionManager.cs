using QuizProject.Data;
using QuizProject.Exception;
using QuizProject.Interfaces;
using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Service
{
    public class QuestionManager : IQuestionService
    {
        private readonly JsonContext _db;
        private List<MultipleChoiceQuestion> _questions;
        
        public QuestionManager()
        {
            _db = new JsonContext();
            _questions = _db.LoadQuestions();
        }

        public void AddQuestion(MultipleChoiceQuestion question)
        {
            _questions.Add(question);
            _db.SaveQuestions(_questions);
        }

        public void DeleteQuestion(Guid id)
        {
            var questionToRemove = _questions.FirstOrDefault(q => q.Id == id);

            if (questionToRemove == null)
            {
                throw new QuestionNotFoundException($"Sual tapilmadi! ID : {id}");
            }

            _questions.Remove(questionToRemove);
            _db.SaveQuestions(_questions);
        }

        public List<MultipleChoiceQuestion> GetQuestionsByDifficulty(QuizProject.Enum.Difficulty difficulty)
        {
            return _questions.Where(q => q.DifficultyLevel == difficulty).ToList();
        }
    }
}
