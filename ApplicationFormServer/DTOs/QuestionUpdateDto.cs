namespace ApplicationFormServer.DTOs
{
    public class QuestionUpdateDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string QuestionType { get; set; }
        public bool EnableOtherOption { get; set; }
        public int? MaxChoiceAllowed { get; set; }
        public List<ChoiceUpdateDto>? Choices { get; set; }
    }
}
