using System;
namespace Gamification.Pages
{
	public partial class PlayQuiz
	{
		private int QuizId { get; set; }

		public PlayQuiz()
		{
		}

		public void OnClickQuiz(int quizId)
		{
			
			QuizId = quizId;
			
		}
	}
}

