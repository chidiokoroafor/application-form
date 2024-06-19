using ApplicationFormServer.Models;

namespace ApplicationFormServer.DTOs
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public string QuestionType { get; set; }
        public bool EnableOtherOption { get; set; }
        public int? MaxChoiceAllowed { get; set; }
        public List<ChoiceDto>? Choices { get; set; }
    }
}
