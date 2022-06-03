namespace Gamification.Models
{
	public class Quiz
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Subject Subject { get; set; }
		public string Owner { get; set; }
		public bool Concept { get; set; }
		public bool OpenToFreePlay { get; set; }
		public bool OpenToKnowledgeBase { get; set; }
	}
}

