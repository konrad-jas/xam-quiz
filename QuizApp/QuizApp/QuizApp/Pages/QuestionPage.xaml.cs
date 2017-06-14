using MvvmCross.Forms.Core;
using Xamarin.Forms.Xaml;

namespace QuizApp.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestionPage : MvxContentPage
	{
		public QuestionPage()
		{
			InitializeComponent();
		}
	}
}