using System;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class SelectQuiz
	{
		private List<Faculty> ListOfFaculties { get; set; }


		private List<Subject> ListOfSubjects { get; set; }


		private List<Quiz> ListOfQuizzes { get; set; }

		[Inject] IQuizService QuizService { get; set; }
        [Inject] IFacultyService FacultyService { get; set; }
        [Inject] ISubjectService SubjectService { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ListOfFaculties = FacultyService.GetFaculties();
        }


        void OnSelectFaculty(ChangeEventArgs e)
		{
			ListOfQuizzes = null;
			ListOfSubjects = SubjectService.GetSubjects(Int32.Parse(e.Value.ToString()));
		}

		void OnSelectSubject(ChangeEventArgs e)
		{
            Subject currentSubject = new Subject();

            foreach (Subject subject in ListOfSubjects)
            {
                if (subject.Id.ToString() == e.Value.ToString())
                {
                    currentSubject = subject;
                }
            }

            ListOfQuizzes = QuizService.GetQuizzes(currentSubject);
		}

		public void GoToShowQuiz(Quiz quiz)
        {
			QuizService.SetCurrentQuiz(quiz);
            NavigationManager.NavigateTo("PlayQuiz");
        }


	}
}

