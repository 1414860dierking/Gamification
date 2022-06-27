using System;
using Microsoft.Data.SqlClient;
using Gamification.Models;
using Gamification.Services;
using Microsoft.AspNetCore.Components;

namespace Gamification.Pages
{
	public partial class PlayQuiz
	{
		bool AnswerMode { get; set; }
		bool CorrectAnswer { get; set; }
		bool QuizActive { get; set; }

		List<Question> ListOfQuestions { get; set; }
		List<Answer> ListOfAnswers { get; set; }
		int QuestionNumber { get; set; }

		Question CurrentQuestion { get; set; }

		List<Answer> CurrentAnswers { get; set; }

		List<KnowledgeBaseItem> KnowledgeBaseItems { get; set; }

		Quiz Quiz { get; set; }

		int CorrectAnswerCount { get; set; }
		Answer ChosenAnswer { get; set; }

		[Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IQuizService QuizService { get; set; }
        [Inject] IKnowledgeBaseService KnowledgeBaseService { get; set; }



        SqlConnection sqlConnection;

		


		public PlayQuiz()
		{
			CorrectAnswerCount = 0;
			QuestionNumber = 0;
			AnswerMode = false;
			CorrectAnswer = false;
			QuizActive = false;
        }


		protected override async Task OnInitializedAsync()
		{
            ListOfQuestions = QuizService.GetQuestions();
            ListOfAnswers = QuizService.GetAnswers(ListOfQuestions);
            KnowledgeBaseItems = KnowledgeBaseService.GetKnowledgeBaseItems(ListOfQuestions);
            Quiz = QuizService.GetCurrentQuiz();
            LoadQuestion();
		}

		private void LoadQuestion()
        {
			AnswerMode = false;
			QuestionNumber++;
			if (QuestionNumber <= ListOfQuestions.Count())
			{
				if (ListOfQuestions != null && ListOfQuestions.Count() > 0)
				{
					foreach (Question question in ListOfQuestions)
					{
						if (question.SequenceNumber == QuestionNumber)
						{
							List<Answer> answers = new List<Answer>();
							CurrentQuestion = question;
							foreach (Answer answer in ListOfAnswers)
							{
								if (answer.Question.Id == question.Id)
								{
									answers.Add(answer);
								}
							}
							StateHasChanged();
							CurrentAnswers = answers;
							break;

						}

					}
				}
			}
			else
			{
				StateHasChanged();
			}
        }

		private void ClickAnswer(Answer answer)
        {
			if (answer.CorrectAnswer)
            {
				AnswerMode = true;
				CorrectAnswer = true;
				CorrectAnswerCount++;
				ChosenAnswer = answer;
            }
            else
            {
				AnswerMode = true;
				CorrectAnswer = false;
				ChosenAnswer = answer;
			}
			StateHasChanged();
        }

		private void Home()
        {
			NavigationManager.NavigateTo("/");
		}

		private void StartQuiz()
        {
			QuizActive = true;
        }
	}
}

