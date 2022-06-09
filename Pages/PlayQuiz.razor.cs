using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class PlayQuiz
	{
		private Quiz Quiz { get; set; }

		private List<Quiz> ListOfQuizzes { get; set; }

		private List<Question> ListOfQuestions { get; set; }

		SqlConnection sqlConnection;

		[Inject] QuizService QuizService { get; set; }


		public PlayQuiz()
		{
			ListOfQuestions = new List<Question>();
			ListOfQuizzes = new List<Quiz>();
			
		}

		protected override async Task OnInitializedAsync()
        {
			GetListOfquestions();
		}


		private void GetListOfquestions()
		{

			string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			sqlConnection = new SqlConnection(sqlconn);


			try
			{
				sqlConnection.Open();

				List<Question> questions = new List<Question>();

				string queryString = $"SELECT * FROM dbo.Questions WHERE QuizzenId = {QuizService.Quiz.Id}";
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
					}); ; 
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
	}
}

