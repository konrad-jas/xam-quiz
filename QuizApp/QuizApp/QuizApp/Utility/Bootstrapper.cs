using Ninject;
using QuizApp.ViewModels;
using QuizApp.Views;
using Xamarin.Forms;

namespace QuizApp.Utility
{
	public static class Bootstrapper
	{
		public static void _initialize(StandardKernel container)
		{
			var navigationPage = (NavigationPage) Application.Current.MainPage;
			var navigationService = new NavigationService(navigationPage);
			container.Bind<INavigationService>().ToConstant(navigationService);

			RegisterViews(container);
			RegisterViewModels(container);
		}

		private static void RegisterViewModels(StandardKernel container)
		{
			container.Bind<QuestionViewModel>().ToSelf();
		}

		private static void RegisterViews(StandardKernel container)
		{
			container.Bind<QuestionView>().ToSelf();
		}
	}
}
