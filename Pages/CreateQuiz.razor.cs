using System;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
    public partial class CreateQuiz
    {
		private string InputValueQuizName;

		private string InputValueQuizDescription;

		private bool CheckboxIsConcept;

		private bool CheckboxIsOpenToFreePlay;

		private bool CheckboxIsOpenToKnowledgeBase;

		private string Question;

		private string QuestionDescription;

		private string QuestionTimeToAnswer;	

		private string Answertype1;
		private string Answertype2;
		private string Answertype3;
		private string Answertype4;

		private string Antwoordvalue1;
		private string Antwoordvalue2;
		private string Antwoordvalue3;
		private string Antwoordvalue4;

		private bool Checkbox1RightAnswer;
		private bool Checkbox2RightAnswer;
		private bool Checkbox3RightAnswer;
		private bool Checkbox4RightAnswer;



		private List<Faculty> ListOfFaculties { get; set; }
		private List<Subject> ListOfSubjects { get; set; }

		[Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IQuizService QuizService { get; set; }
        [Inject] IFacultyService FacultyService { get; set; }
        [Inject] ISubjectService SubjectService { get; set; }


        public CreateQuiz()
        {
			CheckboxIsOpenToFreePlay = true;
			CheckboxIsOpenToKnowledgeBase = true;
        }

        protected override async Task OnInitializedAsync()
        {
            ListOfFaculties = FacultyService.GetFaculties();
        }


        void OnSelectFaculty(ChangeEventArgs e)
		{
            ListOfSubjects = SubjectService.GetSubjects(Int32.Parse(e.Value.ToString()));
        }

		void OnSelectSubject(ChangeEventArgs e)
		{
            
        }

		private void SaveQuiz()
        {
			//TODO: opslaan van de quiz moet hier
        }



	}
}
