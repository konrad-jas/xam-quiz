using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuizApp.Core.DTOs
{
	public class TriviaRootDTO
	{
		[JsonProperty("response_code")]
		public int ResponseCode { get; set; }

		[JsonProperty("results")]
		public List<TriviaQuestionDTO> Results { get; set; }
	}
}
