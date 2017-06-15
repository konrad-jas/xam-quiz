using Newtonsoft.Json;

namespace QuizApp.Core.DTOs
{
	public class TokenResetDTO
	{
		[JsonProperty("response_code")]
		public int ResponseCode { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }
	}
}
