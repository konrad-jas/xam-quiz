using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Forms.Droid;
using MvvmCross.Platform;
using QuizApp.Core.Services;
using QuizApp.Droid.Services;

namespace QuizApp.Droid
{
	public class Setup : MvxFormsAndroidSetup
	{
		public Setup(Context applicationContext)
			: base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			Mvx.LazyConstructAndRegisterSingleton<ICameraService, CameraService>();
			Mvx.LazyConstructAndRegisterSingleton<IToastService, ToastService>();
			return new Core.App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}
	}
}
