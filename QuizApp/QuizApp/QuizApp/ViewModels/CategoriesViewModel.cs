using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.POs;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class CategoriesViewModel : MvxViewModel
	{
		private readonly ICategoriesService _categoriesService;
		private readonly IMvxNavigationService _navigationService;

		public CategoriesViewModel(ICategoriesService categoriesService, IMvxNavigationService navigationService)
		{
			_categoriesService = categoriesService;
			_navigationService = navigationService;

			SelectCategoryCommand = new MvxCommand<CategoryPO>(SelectCategoryAction);
			ConfirmCategoryCommand = new MvxAsyncCommand(ConfirmCategoryAction, AnyCategorySelected);

			Categories = new List<CategoryPO>();
		}

		public override void Start()
		{
			Categories = _categoriesService.GetCategories();

			foreach (var category in Categories)
			{
				category.SelectCommand = SelectCategoryCommand;
			}
		}

		public IEnumerable<CategoryPO> Categories { get; set; }

		public IMvxCommand SelectCategoryCommand { get; }
		private void SelectCategoryAction(CategoryPO selectedCategory)
		{
			if (selectedCategory.Selected)
				return;

			foreach (var category in Categories)
			{
				category.Selected = false;
			}

			selectedCategory.Selected = true;
			ConfirmCategoryCommand.RaiseCanExecuteChanged();
		}

		public IMvxAsyncCommand ConfirmCategoryCommand { get; }
		private async Task ConfirmCategoryAction()
		{
			await _navigationService.Navigate<QuestionViewModel>();
		}

		private bool AnyCategorySelected()
			=> Categories.Any(x => x.Selected);
	}
}
