using System;
using Gamification.Models;
using Microsoft.Data.SqlClient;
namespace Gamification.Services
{
	public class FacultyService: IFacultyService
	{
        SqlConnection sqlConnection;

        public List<Faculty> Faculties;

        public List<Faculty> GetFaculties()
        {
            if (Faculties == null || Faculties.Count == 0)
            {
                string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                sqlConnection = new SqlConnection(sqlconn);

                Faculties = new List<Faculty>();

                try
                {
                    sqlConnection.Open();

                    string queryString = $"SELECT * FROM dbo.Faculties";
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Faculties.Add(new Faculty()
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"]),
                            Name = sqlDataReader["Name"].ToString()
                        });

                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Faculties;
        }
    }
}

