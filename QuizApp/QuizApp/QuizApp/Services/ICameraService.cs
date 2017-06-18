using System.Threading.Tasks;

namespace QuizApp.Core.Services
{
	public interface ICameraService
	{
		Task<byte[]> TakePhoto();
	}
}
