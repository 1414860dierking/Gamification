using System;
using Gamification.Models;
namespace Gamification.Services
{
	public interface IKnowledgeBaseService
	{
        public List<KnowledgeBaseItem> GetKnowledgeBaseItems(List<Question> questions);
    }
}

