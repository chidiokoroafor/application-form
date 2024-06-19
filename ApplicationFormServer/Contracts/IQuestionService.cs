using ApplicationFormServer.DTOs;
using ApplicationFormServer.Models;

namespace ApplicationFormServer.Contracts
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsByTypeAsync(string query);
        Task<Question> GetQuestionByIdAsync(string id);
        Task AddQuestionAsync(QuestionDto item);
        Task UpdateQuestionAsync(string id, QuestionUpdateDto item);
    }
}
