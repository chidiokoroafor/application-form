using ApplicationFormServer.Contracts;
using ApplicationFormServer.DTOs;
using ApplicationFormServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationFormServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(string id)
        {
            return Ok(await _questionService.GetQuestionByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object request");
            }

            await _questionService.AddQuestionAsync(question);
            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditQuestion([FromBody] QuestionUpdateDto question, string id)
        {
            if (question.Id != id)
            {
                return BadRequest("Invalid request");
            }
            await _questionService.UpdateQuestionAsync(id, question);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionsbyType(string questionType)
        {
            return Ok(await _questionService.GetAllQuestionsByTypeAsync(questionType));
        }
    }
}
