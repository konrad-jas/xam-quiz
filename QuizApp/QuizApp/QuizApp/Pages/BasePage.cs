using MvvmCross.Forms.Core;
using Xamarin.Forms;

namespace QuizApp.Core.Pages
{
	public class BasePage : MvxContentPage
	{
		public BasePage()
		{
			Title = "QuizApp";
			NavigationPage.SetHasBackButton(this, false);
		}
	}
}
