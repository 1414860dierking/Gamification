using Gamification.Models;
namespace Gamification.Services
{
    public class QuizService
    {
        public Quiz Quiz { get; private set; }

        public async Task Set(Quiz quiz)
        {
            Quiz = quiz;
        }
    }
}
