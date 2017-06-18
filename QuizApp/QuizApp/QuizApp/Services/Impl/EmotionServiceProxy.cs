using System.IO;
using System.Threading;
using System.Threading.Tasks;
using QuizApp.Core.DTOs;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionServiceProxy : BaseRestProxy, IEmotionServiceProxy
	{
		private readonly IEmotionRecognitionClient _client;

		public EmotionServiceProxy(IEmotionRecognitionClient client)
		{
			_client = client;
		}

		public async Task<DetectedEmotionsDTO> PostPhotoAsync(Stream photoStream, CancellationToken token = default(CancellationToken))
		{
			return await FetchAsync(async () => await _client.PostPhotoAsync(photoStream, token), () => null).ConfigureAwait(false);
		}
	}
}
