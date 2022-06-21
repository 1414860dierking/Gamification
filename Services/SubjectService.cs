using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Microsoft.AspNetCore.Components;

namespace Gamification.Services
{
    public class SubjectService : ISubjectService
    {
        SqlConnection sqlConnection;

        public IFacultyService FacultyService;

        public SubjectService(IFacultyService facultyService)
        {
            FacultyService = facultyService;
        }

        public List<Subject> GetSubjects(int facultyId)
        {
            string sqlconn = "Server=tcp:gamification-dev.database.windows.net,1433;Initial Catalog=Gamification-Dev;Persist Security Info=False;User ID=gamification;Password=rVRm7VSymNs5NUj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            List<Subject> subjects = new List<Subject>();
            Faculty currentFaculty = new Faculty();
            List<Faculty> faculties = FacultyService.GetFaculties();

            foreach (Faculty faculty in faculties)
            {
                if (faculty.Id == facultyId)
                {
                    currentFaculty = faculty;
                }
            }

            try
            {
                sqlConnection.Open();

                string queryString = $"SELECT * FROM dbo.Subjects WHERE FacultiesId = {facultyId}";
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

            }
            catch (Exception)
            {
                throw;
            }

            return subjects;
        }
    }
}

