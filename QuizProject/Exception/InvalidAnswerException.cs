using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Exception
{
    public class InvalidAnswerException : System.Exception
    {
        public InvalidAnswerException(string message) : base(message) { }
    }
}
