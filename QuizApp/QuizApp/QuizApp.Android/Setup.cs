using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Forms.Droid;

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
			return new Core.App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}
	}
}
