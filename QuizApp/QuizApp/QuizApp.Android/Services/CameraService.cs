using System.IO;
using System.Threading.Tasks;
using Android.Hardware;
using QuizApp.Core.Services;
using QuizApp.Droid.NativeImpl;
using QuizApp.Droid.Utils;

namespace QuizApp.Droid.Services
{
	public class CameraService : ICameraService
	{
		public async Task<Stream> TakePhoto()
		{
			var cameraId = FindFrontFacingCamera();
			var camera = Camera.Open(cameraId);
			camera.StartPreview();
			var callback = new PhotoCallback();
			camera.TakePicture(null, null, callback);
			var photo = await callback.GetPictureData();
			camera.Release();
			return await ImageUtils.Preprocess(photo);
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