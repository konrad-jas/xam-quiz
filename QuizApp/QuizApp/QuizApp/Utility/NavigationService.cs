using System;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using QuizApp.ViewModels.Base;
using Xamarin.Forms;

namespace QuizApp.Utility
{
	public class NavigationService : INavigationService
	{
		private readonly NavigationPage _navigationPage;

		public NavigationService(NavigationPage navigationPage)
		{
			_navigationPage = navigationPage;
		}

		public async Task ShowViewModel<TViewModel>() where TViewModel : BaseViewModel
		{
			try
			{
				var viewModel = App.Resolver.Get<TViewModel>();
				var viewType = GetCorrespondingViewType<TViewModel>();
				var view = (Page) App.Resolver.Get(viewType);
				view.BindingContext = viewModel;
				await _navigationPage.PushAsync(view, true);
			}
			catch (ArgumentNullException)
			{
				throw new InvalidOperationException($"Could not resolve view for {typeof(TViewModel).Name}");
			}
		}

		private Type GetCorrespondingViewType<TViewModel>()
		{
			var viewModelType = typeof(TViewModel);
			var viewModelTypeName = viewModelType.AssemblyQualifiedName;
			var splitName = viewModelTypeName.Split(','); //resolve view type name by naming convention
			splitName[0] = viewModelType.FullName.Replace("ViewModels", "Views").Replace("Model", "");
			var viewType = Type.GetType(string.Join(",", splitName));
			return viewType;
		}
	}
}
