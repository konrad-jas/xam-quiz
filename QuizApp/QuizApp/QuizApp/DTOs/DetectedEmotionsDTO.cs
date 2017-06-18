using Newtonsoft.Json;

namespace QuizApp.Core.DTOs
{
	public class DetectedEmotionsDTO
	{
		[JsonProperty("anger")]
		public double Anger { get; set; }
		[JsonProperty("contempt")]
		public double Contempt { get; set; }
		[JsonProperty("happiness")]
		public double Happiness { get; set; }
		[JsonProperty("sadness")]
		public double Sadness { get; set; }
	}
}
