using System;
using Gamification.Models;
using Microsoft.AspNetCore.SignalR;

namespace Gamification.Hubs
{
	public class QuizHub : Hub
	{
		public async Task SendQuestion(string title, string description, List<string> answerType, List<string> answerValue, int timeToAnswer)
		{
			await Clients.All.SendAsync("Question", title, description, answerType, answerValue, timeToAnswer);
        }

        public async Task SendAnswer(string answer)
        {
            await Clients.All.SendAsync("Answer", Context.ConnectionId, answer);
        }

        public async Task SendCorrection(string answerType, string answerValue)
        {
            await Clients.All.SendAsync("Correction", answerType, answerValue);
        }

        public async Task StartQuiz()
        {
            await Clients.All.SendAsync("Start");
        }

        public async Task EndQuiz()
        {
            await Clients.All.SendAsync("End");
        }

        public async Task AddToGroup(string groupName, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Join", Context.ConnectionId, user);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Left", Context.ConnectionId);
        }
    }
}

