using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizApp.Core.DTOs;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionRecognitionClient : IEmotionRecognitionClient
	{
		private readonly HttpClient _client;

		public EmotionRecognitionClient(HttpClient client)
		{
			_client = client;
		}

		public async Task<DetectedEmotionsDTO> PostPhotoAsync(Stream photoStream, CancellationToken token = default(CancellationToken))
		{
			var result = await Task.Run(async () => await PostInternal(photoStream, token), token);
			var emotionResult = JsonConvert.DeserializeObject<List<EmotionResultDTO>>(result).First();
			return emotionResult.DetectedEmotions;
		}

		private async Task<string> PostInternal(Stream photoStream, CancellationToken token)
		{
			var content = new StreamContent(photoStream);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			var response = await _client.PostAsync("/emotion/v1.0/recognize", content, token);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
	}
}