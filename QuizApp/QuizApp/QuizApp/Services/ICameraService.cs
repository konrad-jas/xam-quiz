using System.IO;
using System.Threading.Tasks;

namespace QuizApp.Core.Services
{
	public interface ICameraService
	{
		Task<Stream> TakePhoto();
	}
}
