using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace QuizProject.Data
{
    public class JsonContext
    {
        private readonly string _questionsFilePath = "question.json";
        private readonly string _historyFilePath = "history.json";

        private readonly JsonSerializerOptions _options;

        public JsonContext() 
        { 
            _options = new JsonSerializerOptions();
        }

        public void SaveQuestions(List<MultipleChoiceQuestion> questions)
        {
            string jsonString = JsonSerializer.Serialize(questions, _options);
            File.WriteAllText(_questionsFilePath, jsonString);
        }

        public List<MultipleChoiceQuestion> LoadQuestions()
        {
            if (!File.Exists(_questionsFilePath))
            {
                return new List<MultipleChoiceQuestion>();
            }

            string jsonString = File.ReadAllText(_questionsFilePath);
            return JsonSerializer.Deserialize<List<MultipleChoiceQuestion>>(jsonString) ?? new List<MultipleChoiceQuestion>();
        }

        public void SaveHistory(List<Score> history)
        {
            string jsonString = JsonSerializer.Serialize(history, _options);
            File.WriteAllText(_historyFilePath, jsonString);
        }

        public List<Score> LoadHistory()
        {
            if (!File.Exists(_historyFilePath)) return new List<Score>();

            string jsonString = File.ReadAllText(_historyFilePath);
            return JsonSerializer.Deserialize<List<Score>>(jsonString) ?? new List<Score>();
        }
    }
}
