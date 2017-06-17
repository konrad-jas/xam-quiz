using System.Threading.Tasks;
using Android.Hardware;
using QuizApp.Core.Services;
using QuizApp.Droid.NativeImpl;

namespace QuizApp.Droid.Services
{
	public class CameraService : Java.Lang.Object, ICameraService
	{
		public async Task<byte[]> TakePhoto()
		{
			var cameraId = FindFrontFacingCamera();
			var camera = Camera.Open(cameraId);
			camera.StartPreview();
			var callback = new PhotoCallback();
			camera.TakePicture(null, null, callback);
			camera.Release();
			return await callback.GetPictureData();
		}

		private int FindFrontFacingCamera()
		{
			var cameraId = -1;
			var numberOfCameras = Camera.NumberOfCameras;
			for (int i = 0; i < numberOfCameras; i++)
			{
				Camera.CameraInfo info = new Camera.CameraInfo();
				Camera.GetCameraInfo(i, info);
				if (info.Facing == Camera.CameraInfo.CameraFacingFront)
				{
					cameraId = i;
					break;
				}
			}
			return cameraId;
		}
	}
}