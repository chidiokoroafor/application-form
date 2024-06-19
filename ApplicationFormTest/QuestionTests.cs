using ApplicationFormServer.Contracts;
using ApplicationFormServer.Controllers;
using ApplicationFormServer.DTOs;
using ApplicationFormServer.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFormTest
{
    public class QuestionTests
    {
        private readonly Mock<IQuestionService> _questionServiceMock;
        private readonly QuestionsController _questionsController;
        public QuestionTests()
        {
            _questionServiceMock = new Mock<IQuestionService>();
            _questionsController = new QuestionsController(_questionServiceMock.Object);
        }

        [Fact]
        public async void GetById_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var question = new Question
            {
                Id = "gd9iy8yyuhguty74yydgrt4trgr",
                Title = "Title",
                EnableOtherOption = true,
                MaxChoiceAllowed = 1,
            };

            _questionServiceMock.Setup(repo => repo.GetQuestionByIdAsync("gd9iy8yyuhguty74yydgrt4trgr")).ReturnsAsync(question);


            // Act
            var okResult = await _questionsController.GetQuestion("gd9iy8yyuhguty74yydgrt4trgr");

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void GetById_WhenCalled_ReturnsCorrectType()
        {
            //Arrange
            var question = new Question
            {
                Id = "gd9iy8yyuhguty74yydgrt4trgr",
                Title = "Title",
                EnableOtherOption = true,
                MaxChoiceAllowed = 1,
            };

            _questionServiceMock.Setup(repo => repo.GetQuestionByIdAsync("gd9iy8yyuhguty74yydgrt4trgr")).ReturnsAsync(question);


            // Act
            var okResult = await _questionsController.GetQuestion("gd9iy8yyuhguty74yydgrt4trgr");


            //Assert
            var item = Assert.IsType<OkObjectResult>(okResult as OkObjectResult).Value;
            Assert.IsType<Question>(item);
        }

        [Fact]
        public async void GetByQuestionType_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            _questionServiceMock.Setup(repo => repo.GetAllQuestionsByTypeAsync("Pragraph")).ReturnsAsync(new List<Question>());


            // Act
            var okResult = await _questionsController.GetQuestionsbyType("Pragraph");

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void CreateQuestion_InvalidModel_CreateQuestionNotCalled()
        {
            _questionsController.ModelState.AddModelError("Name", "Title is Required");
            //Arrange
            var newQuestion = new QuestionDto
            {
                MaxChoiceAllowed = 1,
                EnableOtherOption = false,
            };

            //act
            await _questionsController.CreateQuestion(newQuestion);

            //Assert
            _questionServiceMock.Verify(x => x.AddQuestionAsync(It.IsAny<QuestionDto>()), Times.Never);

        }


        [Fact]
        public async void CreateQuestion_validModel_CreateQuestionCalled()
        {
            //Arrange
            QuestionDto? quest = null;
            _questionServiceMock.Setup(x => x.AddQuestionAsync(It.IsAny<QuestionDto>())).Callback<QuestionDto>(x => quest = x);
            var newQuestion = new QuestionDto
            {
                Title = "Your name",
                QuestionType = "Paragraph",
                MaxChoiceAllowed = 1,
                EnableOtherOption = false,
            };

            //act
            await _questionsController.CreateQuestion(newQuestion);

            //Assert
            _questionServiceMock.Verify(x => x.AddQuestionAsync(It.IsAny<QuestionDto>()), Times.Once);

            Assert.Equal(quest.Title, newQuestion.Title);
            Assert.Equal(quest.QuestionType, newQuestion.QuestionType);

        }


        [Fact]
        public async void EditQuestion_InvalidId_ReturnsBadRequest()
        {
            _questionsController.ModelState.AddModelError("Name", "Title is Required");
            //Arrange
            var question = new QuestionUpdateDto
            {
                Id = "dw235fr65hth676jy6",
                MaxChoiceAllowed = 1,
                EnableOtherOption = false,
            };

            //act
            var response = await _questionsController.EditQuestion(question, "dgfefefeth6yt7cgf");

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void EditQuestion_InvalidId_UpdateQuestionNeverCalled()
        {
            _questionsController.ModelState.AddModelError("Name", "Title is Required");
            //Arrange
            var question = new QuestionUpdateDto
            {
                Id = "dw235fr65hth676jy6",
                MaxChoiceAllowed = 1,
                EnableOtherOption = false,
            };

            //act
            await _questionsController.EditQuestion(question, "dgfefefeth6yt7cgf");

            //Assert
            _questionServiceMock.Verify(x => x.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionUpdateDto>()), Times.Never);

        }


        [Fact]
        public async void EditQuestion_validModel_CreateQuestionCalled()
        {
            //Arrange
            var question = new QuestionUpdateDto
            {
                Id = "dw235fr65hth676jy6",
                Title = "Your name",
                QuestionType = "Paragraph",
                MaxChoiceAllowed = 1,
                EnableOtherOption = false,
            };

            //act
            await _questionsController.EditQuestion(question, "dw235fr65hth676jy6");

            //Assert
            _questionServiceMock.Verify(x => x.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionUpdateDto>()), Times.Once);



        }
    }
}
