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
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _questionService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDto question)
        {
           
            await _questionService.AddAsync(question);
            return Ok(question);
            //return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] QuestionUpdateDto question, string id)
        {
            if (question.Id != id)
            {
                return BadRequest("Invalid request");
            }
            await _questionService.UpdateAsync(id, question);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> List(string questionType)
        {
            return Ok(await _questionService.GetMultipleAsync(questionType));
        }
    }
}
