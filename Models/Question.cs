using System;
namespace Gamification.Models
{
	public class Question
	{
		public int Id { get; set; }
		public Quiz Quiz { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int TimeToAnswer { get; set; }
		public int SequenceNumber { get; set; }
}
}

