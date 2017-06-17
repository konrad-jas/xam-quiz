using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Droid;
using Xamarin.Forms;
using MvvmCross.Forms.Core;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenters;
using MvvmCross.Core.ViewModels;

namespace QuizApp.Droid
{
	[Activity(Label = "QuizApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
	public class QuizAppActivity : MvxFormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ToolbarResource = Resource.Layout.Toolbar;
			TabLayoutResource = Resource.Layout.Tabbar;

			Forms.Init(this, bundle);
			var mvxFormsApp = new MvxFormsApplication();
			LoadApplication(mvxFormsApp);

			var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsPagePresenter;
			presenter.MvxFormsApp = mvxFormsApp;

			Mvx.Resolve<IMvxAppStart>().Start();
		}
	}
}
