using ApplicationFormServer.DTOs;
using ApplicationFormServer.Models;

namespace ApplicationFormServer.Contracts
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetMultipleAsync(string query);
        Task<Question> GetAsync(string id);
        Task AddAsync(QuestionDto item);
        Task UpdateAsync(string id, QuestionUpdateDto item);
        Task DeleteAsync(string id);
    }
}
