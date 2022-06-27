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

		Quiz quiz = new Quiz();
		private int subject;


		//vraag 1
		private string Question1;
		private string QuestionDescription1;
		private string QuestionTimeToAnswer1;

		Question question1 = new Question();

		private string Answertype11;
		private string Answertype12;
		private string Answertype13;
		private string Answertype14;

		private string Antwoordvalue11;
		private string Antwoordvalue12;
		private string Antwoordvalue13;
		private string Antwoordvalue14;

		private bool Checkbox1RightAnswer1;
		private bool Checkbox2RightAnswer1;
		private bool Checkbox3RightAnswer1;
		private bool Checkbox4RightAnswer1;

		Answer answer11 = new Answer();
		Answer answer12 = new Answer();
		Answer answer13 = new Answer();
		Answer answer14 = new Answer();

		//vraag 2
		private string Question2;
		private string QuestionDescription2;
		private string QuestionTimeToAnswer2;

		Question question2 = new Question();

		private string Answertype21;
		private string Answertype22;
		private string Answertype23;
		private string Answertype24;

		private string Antwoordvalue21;
		private string Antwoordvalue22;
		private string Antwoordvalue23;
		private string Antwoordvalue24;

		private bool Checkbox1RightAnswer2;
		private bool Checkbox2RightAnswer2;
		private bool Checkbox3RightAnswer2;
		private bool Checkbox4RightAnswer2;

		Answer answer21 = new Answer();
		Answer answer22 = new Answer();
		Answer answer23 = new Answer();
		Answer answer24 = new Answer();

		//vraag 3
		private string Question3;
		private string QuestionDescription3;
		private string QuestionTimeToAnswer3;

		Question question3 = new Question();

		private string Answertype31;
		private string Answertype32;
		private string Answertype33;
		private string Answertype34;

		private string Antwoordvalue31;
		private string Antwoordvalue32;
		private string Antwoordvalue33;
		private string Antwoordvalue34;

		private bool Checkbox1RightAnswer3;
		private bool Checkbox2RightAnswer3;
		private bool Checkbox3RightAnswer3;
		private bool Checkbox4RightAnswer3;

		Answer answer31 = new Answer();
		Answer answer32 = new Answer();
		Answer answer33 = new Answer();
		Answer answer34 = new Answer();


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
			subject = Int32.Parse(e.Value.ToString());
		}

		private void SaveQuiz()
		{
			quiz.Name = InputValueQuizName;
			quiz.Description = InputValueQuizDescription;
			quiz.Concept = CheckboxIsConcept;
			quiz.OpenToFreePlay = CheckboxIsOpenToFreePlay;
			quiz.OpenToKnowledgeBase = CheckboxIsOpenToKnowledgeBase;

			//vraag 1
			question1.Quiz = quiz;
			question1.Title = Question1;
			question1.Description = QuestionDescription1;
			question1.TimeToAnswer = Int32.Parse(QuestionTimeToAnswer1);
			question1.SequenceNumber = 1;

			answer11.Question = question1;
			answer11.AnswerType = Answertype11;
			answer11.AnswerValue = Antwoordvalue11;
			answer11.CorrectAnswer = Checkbox1RightAnswer1;

			answer12.Question = question1;
			answer12.AnswerType = Answertype12;
			answer12.AnswerValue = Antwoordvalue12;
			answer12.CorrectAnswer = Checkbox2RightAnswer1;

			answer13.Question = question1;
			answer13.AnswerType = Answertype13;
			answer13.AnswerValue = Antwoordvalue13;
			answer13.CorrectAnswer = Checkbox3RightAnswer1;

			answer14.Question = question1;
			answer14.AnswerType = Answertype14;
			answer14.AnswerValue = Antwoordvalue14;
			answer14.CorrectAnswer = Checkbox4RightAnswer1;

			//vraag 2
			question2.Quiz = quiz;
			question2.Title = Question2;
			question2.Description = QuestionDescription2;
			question2.TimeToAnswer = Int32.Parse(QuestionTimeToAnswer2);
			question2.SequenceNumber = 2;

			answer21.Question = question2;
			answer21.AnswerType = Answertype21;
			answer21.AnswerValue = Antwoordvalue21;
			answer21.CorrectAnswer = Checkbox1RightAnswer2;

			answer22.Question = question2;
			answer22.AnswerType = Answertype22;
			answer22.AnswerValue = Antwoordvalue22;
			answer22.CorrectAnswer = Checkbox2RightAnswer2;

			answer23.Question = question2;
			answer23.AnswerType = Answertype23;
			answer23.AnswerValue = Antwoordvalue23;
			answer23.CorrectAnswer = Checkbox3RightAnswer2;

			answer24.Question = question2;
			answer24.AnswerType = Answertype24;
			answer24.AnswerValue = Antwoordvalue24;
			answer24.CorrectAnswer = Checkbox4RightAnswer2;

			//vraag 3
			question3.Quiz = quiz;
			question3.Title = Question3;
			question3.Description = QuestionDescription3;
			question3.TimeToAnswer = Int32.Parse(QuestionTimeToAnswer3);
			question3.SequenceNumber = 3;

			answer31.Question = question3;
			answer31.AnswerType = Answertype31;
			answer31.AnswerValue = Antwoordvalue31;
			answer31.CorrectAnswer = Checkbox1RightAnswer3;

			answer32.Question = question3;
			answer32.AnswerType = Answertype32;
			answer32.AnswerValue = Antwoordvalue32;
			answer32.CorrectAnswer = Checkbox2RightAnswer3;

			answer33.Question = question3;
			answer33.AnswerType = Answertype33;
			answer33.AnswerValue = Antwoordvalue33;
			answer33.CorrectAnswer = Checkbox3RightAnswer3;

			answer34.Question = question3;
			answer34.AnswerType = Answertype34;
			answer34.AnswerValue = Antwoordvalue34;
			answer34.CorrectAnswer = Checkbox4RightAnswer3;

			QuizService.NewQuiz(quiz, subject);

		}
	}
}
