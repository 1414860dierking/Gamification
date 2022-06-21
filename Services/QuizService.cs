using Gamification.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Gamification.Services
{
    public class QuizService : IQuizService
    {
        public Quiz? CurrentQuiz;
        SqlConnection sqlConnection;

        public IConfiguration Configuration;
        public ISubjectService SubjectService;

        public QuizService(IConfiguration configuration, ISubjectService subjectService)
        {
            Configuration = configuration;
            SubjectService = subjectService;
        }

        public void SetCurrentQuiz(Quiz quiz)
        {
            CurrentQuiz = quiz;
        }

        public Quiz GetCurrentQuiz()
        {
            Quiz quiz = CurrentQuiz;
            return quiz;
        }

        public List<Quiz> GetQuizzes(Subject subject)
        {
            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //string sqlconn = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            sqlConnection = new SqlConnection(sqlconn);

            List<Quiz> quizzes = new List<Quiz>();

            try
            {
                sqlConnection.Open();
                string queryString = $"SELECT * FROM dbo.Quizzen WHERE SubjectsId = {subject.Id}";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    quizzes.Add(new Quiz()
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        Name = sqlDataReader["Name"].ToString(),
                        Description = sqlDataReader["Description"].ToString(),
                        Subject = subject,
                        Owner = sqlDataReader["Owner"].ToString(),
                        Concept = (bool)sqlDataReader["Concept"],
                        OpenToFreePlay = (bool)sqlDataReader["OpenToFreePlay"],
                        OpenToKnowledgeBase = (bool)sqlDataReader["OpenToKnowledgeBase"]
                    });
                }
                sqlDataReader.Close();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return quizzes;
        }

        public List<Question> GetQuestions()
        {
            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);
            List<Question> questions = new List<Question>();

            try
            {
                sqlConnection.Open();


                string queryString = $"SELECT * FROM dbo.Questions WHERE QuizzenID = {GetCurrentQuiz().Id}";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    questions.Add(new Question()
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        Quiz = GetCurrentQuiz(),
                        Title = sqlDataReader["Title"].ToString(),
                        Description = sqlDataReader["Description"].ToString(),
                        TimeToAnswer = Convert.ToInt32(sqlDataReader["TimeToAnswer"]),
                        SequenceNumber = Convert.ToInt32(sqlDataReader["SequenceNumber"])

                    });

                }

                sqlDataReader.Close();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return questions;
        }

        public List<Answer> GetAnswers(List<Question> questions)
        {
            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);
            List<Answer> answers = new List<Answer>();


            foreach (Question question in questions)
            {
                sqlConnection.Open();
                string queryString = $"SELECT * FROM dbo.Answers WHERE QuestionsId = {question.Id}";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    answers.Add(new Answer()
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        Question = question,
                        AnswerType = sqlDataReader["AnswerType"].ToString(),
                        AnswerValue = sqlDataReader["AnswerValue"].ToString(),
                        CorrectAnswer = Convert.ToBoolean(sqlDataReader["CorrectAnswer"])
                    });

                }
                sqlDataReader.Close();
                sqlConnection.Close();
            }

            return answers;
        }



    }
}
