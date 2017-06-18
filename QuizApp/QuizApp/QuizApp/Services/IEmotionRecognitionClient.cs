using System.IO;
using System.Threading.Tasks;
using QuizApp.Core.DTOs;

namespace QuizApp.Core.Services
{
	public interface IEmotionRecognitionClient
	{
		Task<DetectedEmotionsDTO> PostPhotoAsync(Stream photoStream);
	}
}
