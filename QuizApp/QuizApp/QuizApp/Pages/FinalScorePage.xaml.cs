using Xamarin.Forms.Xaml;

namespace QuizApp.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinalScorePage
	{
		public FinalScorePage()
		{
			InitializeComponent();
		}

		protected override bool OnBackButtonPressed()
			=> true;
	}
}
