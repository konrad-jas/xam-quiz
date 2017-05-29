using Ninject;
using QuizApp.ViewModels;
using QuizApp.Views;
using Xamarin.Forms;
using QuizApp.Services;
using Refit;
using QuizApp.Services.Impl;
using System.Net.Http;
using ModernHttpClient;
using System;

namespace QuizApp.Utility
{
	public static class Bootstrapper
	{
		public static void _initialize(StandardKernel container)
		{
			var navigationPage = (NavigationPage)Application.Current.MainPage;
			var navigationService = new NavigationService(navigationPage);
			container.Bind<INavigationService>().ToConstant(navigationService);

			var client = new HttpClient(new NativeMessageHandler())
			{
				BaseAddress = new Uri(ExternalServicesURIs.TriviaServiceBaseURI)
			};
			container.Bind<ITriviaServiceClient>().ToMethod(e => RestService.For<ITriviaServiceClient>(client));
			container.Bind<IQuestionsService>().To<QuestionsService>();

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
