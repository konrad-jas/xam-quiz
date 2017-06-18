using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionRecognitionClient : IEmotionRecognitionClient
	{
		private readonly HttpClient _client;

		public EmotionRecognitionClient(HttpClient client)
		{
			_client = client;
		}

		public async Task<string> PostPhotoAsync(Stream photoStream)
		{
			return await Task.Run(async () => await PostInternal(photoStream));
		}

		private async Task<string> PostInternal(Stream photoStream)
		{
			var content = new StreamContent(photoStream);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			var response = await _client.PostAsync("/emotion/v1.0/recognize", content);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
	}
}