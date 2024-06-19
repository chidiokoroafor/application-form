using Newtonsoft.Json;

namespace ApplicationFormServer.Models
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "questionType")]
        public string QuestionType { get; set; }
        [JsonProperty(PropertyName = "enableotheroptions")]
        public bool  EnableOtherOption { get; set; }
        [JsonProperty(PropertyName = "maxchoiceallowed")]
        public int? MaxChoiceAllowed { get; set; }
        [JsonProperty(PropertyName = "choices")]
        public List<Choice>? Choices { get; set;} = new List<Choice>();
    }
}
