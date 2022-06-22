using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
namespace Gamification.Services
{
	public class KnowledgeBaseService: IKnowledgeBaseService
	{
        SqlConnection sqlConnection;

        public List<KnowledgeBaseItem> GetKnowledgeBaseItems(List<Question> questions)
        {
            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);
            List<KnowledgeBaseItem> knowledgeBaseItems = new List<KnowledgeBaseItem>();

            try
            {
                foreach (Question question in questions)
                {
                    sqlConnection.Open();

                    string queryString = $"SELECT * FROM dbo.KnowledgeBaseItems WHERE QuestionsId = {question.Id}";
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        knowledgeBaseItems.Add(new KnowledgeBaseItem()
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"]),
                            Title = sqlDataReader["Title"].ToString(),
                            Description = sqlDataReader["Description"].ToString(),
                            ExternalLink = sqlDataReader["ExternalLink"].ToString(),
                            QuestionsId = Convert.ToInt32(sqlDataReader["QuestionsId"]),
                            QuizzenId = Convert.ToInt32(sqlDataReader["QuizzenId"])

                        });



                    }

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

                
            }
            catch (Exception ex)
            {
                throw;
            }

            return knowledgeBaseItems;
        }
    }
}

