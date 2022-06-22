
using System;
namespace Gamification.Models
{
    public class KnowledgeBaseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExternalLink { get; set; }
        public int QuestionsId { get; set; }

        public int QuizzenId { get; set; }

    }
}
