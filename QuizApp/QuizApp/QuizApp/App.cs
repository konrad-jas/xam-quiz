using System;
using System.Net.Http;
using ModernHttpClient;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using QuizApp.Core.ViewModels;
using QuizApp.Core.Services;
using Refit;

namespace QuizApp.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			var client = new HttpClient(new NativeMessageHandler())
			{
				BaseAddress = new Uri(ExternalServicesURIs.TriviaServiceBaseURI)
			};
			Mvx.LazyConstructAndRegisterSingleton(() => RestService.For<ITriviaServiceClient>(client));

			RegisterAppStart<CategoriesViewModel>();
		}
	}
}