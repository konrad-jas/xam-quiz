using System.IO;
using System.Threading.Tasks;
using Refit;

namespace QuizApp.Core.Services
{
	public interface IEmotionRecognitionClient
	{
		[Post("/emotion/v1.0/recognize")]
		Task<string> PostPhotoAsync([Body] Stream photoStream);
	}
}
