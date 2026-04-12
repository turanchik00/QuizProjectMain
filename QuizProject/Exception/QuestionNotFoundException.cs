using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Exception
{
    public class QuestionNotFoundException : System.Exception
    {
        public QuestionNotFoundException(string message) : base (message) { }
    }
}
