using Newtonsoft.Json;

namespace QuizApp.Core.DTOs
{
	public class EmotionResultDTO
	{
		[JsonProperty("scores")]
		public DetectedEmotionsDTO DetectedEmotions { get; set; }
	}
}
