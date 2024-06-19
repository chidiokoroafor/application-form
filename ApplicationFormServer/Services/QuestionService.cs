using ApplicationFormServer.Contracts;
using ApplicationFormServer.DTOs;
using ApplicationFormServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
//using System.ComponentModel;

namespace ApplicationFormServer.Services
{

    public class QuestionService : IQuestionService
    {
        private Container _container;
        public QuestionService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task AddQuestionAsync(QuestionDto question)
        {
            var newQuestion = new Question
            {
                Id = Guid.NewGuid().ToString(),
                Title = question.Title,
                QuestionType = question.QuestionType,
                EnableOtherOption = question.EnableOtherOption,
                MaxChoiceAllowed = question.MaxChoiceAllowed,
            };

            if (question.Choices != null && question.Choices.Any())
            {

                foreach (var choice in question.Choices)
                {
                    var newChoice = new Choice
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = choice.Name,
                    };
                    newQuestion.Choices.Add(newChoice);
                }
            }

            await _container.CreateItemAsync(newQuestion, new PartitionKey(newQuestion.Id));
        }

        public async Task<Question> GetQuestionByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Question>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsByTypeAsync(string query)
        {
            var parameterizedQuery = new QueryDefinition(
                query: "SELECT * FROM q WHERE (q.questionType = @type)"
            )
                .WithParameter("@type", query);

            using FeedIterator<Question> feed = _container.GetItemQueryIterator<Question>(
                    queryDefinition:parameterizedQuery
                );
            List<Question> results = new List<Question>();
            while (feed.HasMoreResults)
            {
                FeedResponse<Question> response = await feed.ReadNextAsync();
                foreach (Question q in response)
                {
                    results.Add(q);
                }
            }
            return results;
        }

        public async Task UpdateQuestionAsync(string id, QuestionUpdateDto question)
        {
            var targetQuestion = await GetQuestionByIdAsync(id);

            if (targetQuestion != null)
            {
                targetQuestion.QuestionType = question.QuestionType;
                targetQuestion.Title = question.Title;
                targetQuestion.EnableOtherOption = question.EnableOtherOption;
                targetQuestion.MaxChoiceAllowed = question.MaxChoiceAllowed;

                await _container.UpsertItemAsync(targetQuestion, new PartitionKey(id));
            }
           
        }
    }
}
