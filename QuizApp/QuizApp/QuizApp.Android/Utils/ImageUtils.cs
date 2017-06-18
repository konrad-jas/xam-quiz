using System.IO;
using System.Threading.Tasks;
using Android.Graphics;

namespace QuizApp.Droid.Utils
{
	public static class ImageUtils
	{
		public static async Task<Stream> Preprocess(byte[] image)
		{
			using (var bitmap = await BitmapFactory.DecodeByteArrayAsync(image, 0, image.Length))
			{
				var matrix = new Matrix();
				matrix.PostRotate(270);
				using (var rotated = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false))
				{
					var pngStream = new MemoryStream()
					await rotated.CompressAsync(Bitmap.CompressFormat.Png, 100, pngStream);
					return pngStream;
				}
			}
		}
	}
}