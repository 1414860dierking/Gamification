using System;
using Gamification.Models;
namespace Gamification.Services
{
	public interface IQuizService
	{
		public Quiz GetCurrentQuiz();
		public void SetCurrentQuiz(Quiz quiz);
        public List<Quiz> GetQuizzes(Subject subject);
		public List<Question> GetQuestions();
		public List<Answer> GetAnswers(List<Question> questions);
    }
}

