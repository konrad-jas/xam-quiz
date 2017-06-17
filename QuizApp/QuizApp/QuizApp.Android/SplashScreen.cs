using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using Xamarin.Forms;

namespace QuizApp.Droid
{
	[Activity(
		Label = "QuizApp"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		private bool _isInitializationComplete;

		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		public override void InitializationComplete()
		{
			if (!_isInitializationComplete)
			{
				_isInitializationComplete = true;
				StartActivity(typeof(QuizAppActivity));
			}
		}

		protected override void OnCreate(Android.OS.Bundle bundle)
		{
			Forms.Init(this, bundle);

			base.OnCreate(bundle);
		}
	}
}
