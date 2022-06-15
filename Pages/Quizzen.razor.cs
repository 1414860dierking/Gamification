using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class Quizzen
	{
		private List<Faculty> ListOfFaculties { get; set; }

		private int SelectedFaculty { get; set; }

		private List<Subject> ListOfSubjects { get; set; }

		private int SelectedSubject { get; set; }

		private List<Quiz> ListOfQuizzes { get; set; }

		private PlayQuiz PlayQuiz { get; set; }

		SqlConnection sqlConnection;

		[Inject] QuizService QuizService { get; set; }

		[Inject] NavigationManager NavigationManager { get; set; }


		public Quizzen()
		{
			PlayQuiz = new PlayQuiz();
			GetListOfFaculties();
		}

		private void GetListOfFaculties()
		{
			string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			sqlConnection = new SqlConnection(sqlconn);


			try
			{
				sqlConnection.Open();

				List<Faculty> faculties = new List<Faculty>();

				string queryString = $"SELECT * FROM dbo.Faculties";
				SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					faculties.Add(new Faculty()
					{
						Id = Convert.ToInt32(sqlDataReader["Id"]),
						Name = sqlDataReader["Name"].ToString()
					});

				}
				sqlDataReader.Close();
				sqlConnection.Close();
				ListOfFaculties = faculties;
			}
			catch (Exception)
			{
				throw;
			}


		}


		void OnSelectFaculty(ChangeEventArgs e)
		{
			ListOfQuizzes = null;
			SelectedFaculty = Int32.Parse(e.Value.ToString());

			string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			sqlConnection = new SqlConnection(sqlconn);


			try
			{
				sqlConnection.Open();

				List<Subject> subjects = new List<Subject>();

				Faculty currentFaculty = new Faculty();

				foreach (Faculty faculty in ListOfFaculties)
				{
					if (faculty.Id == SelectedFaculty)
					{
						currentFaculty = faculty;
					}
				}

				string queryString = $"SELECT * FROM dbo.Subjects WHERE FacultiesId = {SelectedFaculty}";
				SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

				while (sqlDataReader.Read())
				{
					subjects.Add(new Subject()
					{
						Id = Convert.ToInt32(sqlDataReader["Id"]),
						Name = sqlDataReader["Name"].ToString(),
						Faculty = currentFaculty
					});
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				ListOfSubjects = subjects;
			}
			catch (Exception)
			{
				throw;
			}
		}

		void OnSelectSubject(ChangeEventArgs e)
		{
			SelectedSubject = Int32.Parse(e.Value.ToString());

			string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			sqlConnection = new SqlConnection(sqlconn);


			try
			{
				sqlConnection.Open();

				List<Quiz> quizzes = new List<Quiz>();

				Subject currentSubject = new Subject();

				foreach (Subject subject in ListOfSubjects)
				{
					if (subject.Id == SelectedSubject)
					{
						currentSubject = subject;
					}
				}

				string queryString = $"SELECT * FROM dbo.Quizzen WHERE SubjectsId = {SelectedSubject}";
				SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

				while (sqlDataReader.Read())
				{
					quizzes.Add(new Quiz()
					{
						Id = Convert.ToInt32(sqlDataReader["Id"]),
						Name = sqlDataReader["Name"].ToString(),
						Description = sqlDataReader["Description"].ToString(),
						Subject = currentSubject,
						Owner = sqlDataReader["Owner"].ToString(),
						Concept = (bool)sqlDataReader["Concept"],
						OpenToFreePlay = (bool)sqlDataReader["OpenToFreePlay"],
						OpenToKnowledgeBase = (bool)sqlDataReader["OpenToKnowledgeBase"]
					});
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				ListOfQuizzes = quizzes;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async void GoToShowQuiz(Quiz quiz)
        {
			await QuizService.Set(quiz);
            NavigationManager.NavigateTo("ShowQuiz");
        }


	}
}

