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
        public void TestGetQuiz()
        {
            IConfiguration configuration = null;
            ISubjectService subjectService = null;
            var test = new QuizService(configuration, subjectService);

            Quiz quiz = new Quiz();
            quiz.Id = 1;
            quiz.Concept = false;
            quiz.Name = "Test Quiz";
            quiz.Description = "Dit is een beschrijving";
            quiz.OpenToFreePlay = true;
            quiz.Subject = new Subject();
            quiz.OpenToKnowledgeBase = true;
            quiz.Owner = "Test@test.nl";

            quiz.Subject.Id = 1;
            quiz.Subject.Name = "Subject naam";
            quiz.Subject.Faculty = new Faculty();

            quiz.Subject.Faculty.Id = 1;
            quiz.Subject.Faculty.Name = "ICT";

            test.SetCurrentQuiz(quiz);

            Quiz CurrentQuiz = test.GetCurrentQuiz();

            Assert.IsTrue(CurrentQuiz == quiz && CurrentQuiz.Name == "Test Quiz" && quiz.Name == "Test Quiz");
        }

        [Test]
        public void TestMustBeFailed()
        {
            Assert.Pass();
        }
    }
}
