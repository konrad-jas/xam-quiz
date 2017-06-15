using Newtonsoft.Json;

namespace QuizApp.Core.DTOs
{
	public class TokenRequestDTO
	{
		[JsonProperty("response_code")]
		public int ResponseCode { get; set; }

		[JsonProperty("response_message")]
		public string ResponseMessage { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }
	}
}
