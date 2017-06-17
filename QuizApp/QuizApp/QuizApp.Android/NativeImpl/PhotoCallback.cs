using System.Threading.Tasks;
using Android.Hardware;

namespace QuizApp.Droid.NativeImpl
{
	public class PhotoCallback : Java.Lang.Object, Camera.IPictureCallback
	{
		private readonly TaskCompletionSource<byte[]> _tcs;

		public PhotoCallback()
		{
			_tcs = new TaskCompletionSource<byte[]>();
		}

		public Task<byte[]> GetPictureData()
		{
			return _tcs.Task;
		}

		public void OnPictureTaken(byte[] data, Camera camera)
		{
			_tcs.SetResult(data);
		}
	}
}