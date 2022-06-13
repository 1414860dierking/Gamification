using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class PlayQuiz
	{
		bool AnswerMode { get; set; }
		bool CorrectAnswer { get; set; }

		[Inject] QuizService QuizService { get; set; }

		List<Question> ListOfQuestions { get; set; }
		List<Answer> ListOfAnswers { get; set; }
		int QuestionNumber { get; set; }

		Question CurrentQuestion { get; set; }

		List<Answer> CurrentAnswers { get; set; }

		int CorrectAnswerCount { get; set; }
		Answer ChosenAnswer { get; set; }


		SqlConnection sqlConnection;


		public PlayQuiz()
		{
			CorrectAnswerCount = 0;
			QuestionNumber = 0;
			AnswerMode = false;
			CorrectAnswer = false;
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
				List<Answer> answers = new List<Answer>();

				foreach (Question question in questions)
                {
					sqlConnection.Open();
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
					sqlDataReader.Close();
					sqlConnection.Close();
				}
				ListOfAnswers = answers;
				ListOfQuestions = questions;
			}
			catch (Exception)
			{
				throw;
			}
			LoadQuestion();

		}

		private void LoadQuestion()
        {
			AnswerMode = false;
			QuestionNumber++;
			if (QuestionNumber <= ListOfQuestions.Count())
			{
				if (ListOfQuestions != null && ListOfQuestions.Count() > 0)
				{
					foreach (Question question in ListOfQuestions)
					{
						if (question.SequenceNumber == QuestionNumber)
						{
							List<Answer> answers = new List<Answer>();
							CurrentQuestion = question;
							foreach (Answer answer in ListOfAnswers)
							{
								if (answer.Question.Id == question.Id)
								{
									answers.Add(answer);
								}
							}
							StateHasChanged();
							CurrentAnswers = answers;
							break;

						}

					}
				}
				//afvangen indien geen vragen
			}
			else
			{
				//EINDE QUIZ
			}
        }

		private void ClickAnswer(Answer answer)
        {
			if (answer.CorrectAnswer)
            {
				AnswerMode = true;
				CorrectAnswer = true;
				CorrectAnswerCount++;
				ChosenAnswer = answer;
            }
            else
            {
				AnswerMode = true;
				CorrectAnswer = false;
				ChosenAnswer = answer;
			}
			StateHasChanged();
        }

	}
}

