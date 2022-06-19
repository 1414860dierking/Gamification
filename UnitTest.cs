using Gamification.Models;
using NUnit.Framework;
using Gamification.Pages;
using Gamification.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Bunit;

namespace Gamification
{
    public class UnitTest
    {
        [Test]

        public void Test()
        {
            Assert.Pass();
        }

        //[Test]
        public void Faculties_Show_Be_One()
        {
            // AAA

            // Arrange
            var expected = 0;
            var faculties = new List<Faculty>();
            // Act
            //faculties = quizService.GetFaculties();

            // Assert
            Assert.IsTrue(faculties.Count > expected);
        }

        [Test]

        public void Test2MustBeFailed()
        {
            Assert.Pass();
        }

        // Het testen van de database waarbij de vragen opgevraagd worden uit de Questions tabel.
        [Test]
        public void TestDatabaseQuestionsTable()
        {
            //PlayQuiz PlayQuiz = new PlayQuiz();

            //PlayQuiz.GetListOfquestions();
            //Assert.IsNotNull(PlayQuiz.ListOfQuestions);
            SqlConnection sqlConnection;

            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);


            try
            {
                sqlConnection.Open();

                List<Question> questions = new List<Question>();

                string queryString = $"SELECT * FROM dbo.Questions";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    questions.Add(new Question()
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        Quiz = null,
                        Title = sqlDataReader["Title"].ToString(),
                        Description = sqlDataReader["Description"].ToString(),
                        TimeToAnswer = Convert.ToInt32(sqlDataReader["TimeToAnswer"]),
                        SequenceNumber = Convert.ToInt32(sqlDataReader["SequenceNumber"])
                    }); ;
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                //questions;
                Assert.IsNotNull(questions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Het testen van de database waarbij de vragen opgevraagd worden uit de Faculties tabel.
        [Test]
        public void TestDatabaseFacultiesTable()
        {
            SqlConnection sqlConnection;

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
                //questions;
                Assert.IsNotNull(faculties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Test]
        public void TestPage()
        {
            
        }

        [Test]
        public void Test234()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();
            var cut = ctx.RenderComponent<ClickMe>();
            var buttonElement = cut.Find("button");

            // Act
            buttonElement.Click();
            buttonElement.Click(detail: 3, ctrlKey: true);
            buttonElement.Click(new MouseEventArgs());

            // Assert
            // ...
        }
    }
}
