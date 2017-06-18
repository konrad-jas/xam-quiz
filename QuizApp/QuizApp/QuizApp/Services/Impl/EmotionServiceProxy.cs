using System.IO;
using System.Threading.Tasks;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionServiceProxy : BaseRestProxy, IEmotionServiceProxy
	{
		private readonly IEmotionRecognitionClient _client;

		public EmotionServiceProxy(IEmotionRecognitionClient client)
		{
			_client = client;
		}

		public async Task<string> PostPhotoAsync(Stream photoStream)
		{
			return await FetchAsync(async () => await _client.PostPhotoAsync(photoStream), () => null).ConfigureAwait(false);
		}
	}
}
