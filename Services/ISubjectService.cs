using System;
using Gamification.Models;
namespace Gamification.Services
{
	public interface ISubjectService
	{
        public List<Subject> GetSubjects(int facultyId);

    }
}

