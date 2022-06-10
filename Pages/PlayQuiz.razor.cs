using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class PlayQuiz
	{


		[Inject] QuizService QuizService { get; set; }

		List<Question> ListOfQuestions { get; set; }
		List<Answer> ListOfAnswers { get; set; }
		int QuestionNumber { get; set; }

		Question CurrentQuestion { get; set; }

		List<Answer> CurrentAnswers { get; set; }


		SqlConnection sqlConnection;


		public PlayQuiz()
		{
			CurrentAnswers = new List<Answer>();
			QuestionNumber = 0;
		}


		protected override async Task OnInitializedAsync()
		{
			await GetQuestionsAnswers();
		}


		private async Task GetQuestionsAnswers()
		{
			string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			sqlConnection = new SqlConnection(sqlconn);


			try
			{
				sqlConnection.Open();

				List<Question> questions = new List<Question>();

				string queryString = $"SELECT * FROM dbo.Questions WHERE QuizzenID = {QuizService.Quiz.Id}";
				SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					questions.Add(new Question()
					{
						Id = Convert.ToInt32(sqlDataReader["Id"]),
						Quiz = QuizService.Quiz,
						Title = sqlDataReader["Title"].ToString(),
						Description = sqlDataReader["Description"].ToString(),
						TimeToAnswer = Convert.ToInt32(sqlDataReader["TimeToAnswer"]),
						SequenceNumber = Convert.ToInt32(sqlDataReader["SequenceNumber"])

					});

				}

				sqlDataReader.Close();
				sqlConnection.Close();

			
				sqlConnection.Open();

				foreach (Question question in questions)
                {
					List<Answer> answers = new List<Answer>();

					string queryString2 = $"SELECT * FROM dbo.Answers WHERE QuestionsId = {question.Id}";
					SqlCommand sqlCommand2 = new SqlCommand(queryString2, sqlConnection);
					SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
					while (sqlDataReader2.Read())
					{
						answers.Add(new Answer()
						{
							Id = Convert.ToInt32(sqlDataReader2["Id"]),
							Question = question,
							AnswerType = sqlDataReader2["AnswerType"].ToString(),
							AnswerValue = sqlDataReader2["AnswerValue"].ToString(),
							CorrectAnswer = Convert.ToBoolean(sqlDataReader2["CorrectAnswer"])
						});

					}

					ListOfAnswers = answers;
				}

				sqlDataReader.Close();
				sqlConnection.Close();
				ListOfQuestions = questions;
			}
			catch (Exception)
			{
				throw;
			}


		}

		private void LoadQuestion()
        {
			foreach(Question question in ListOfQuestions)
            {
				if (question.SequenceNumber == QuestionNumber)
                {
					List<Answer> answers = new List<Answer>();
					CurrentQuestion = question;
					foreach(Answer answer in ListOfAnswers)
                    {
						if (answer.Question.Id == question.Id)
                        {
							answers.Add(answer);
                        }
                    }
					
                }
				break;
            }
        }

		private void ChosenAnswer(Answer answer)
        {
			QuestionNumber++;
			if (QuestionNumber < ListOfQuestions.Count())
            {
				LoadQuestion();
            }
            else
            {
				//EINDE QUIZ
            }
        }

	}
}

