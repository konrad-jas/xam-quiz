using Android.Widget;
using QuizApp.Core.Services;

namespace QuizApp.Droid.Services
{
	public class ToastService : IToastService
	{
		public void ShowToast(string text)
		{
			Toast.MakeText(Xamarin.Forms.Forms.Context, text, ToastLength.Short).Show();
		}
	}
}