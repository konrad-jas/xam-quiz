using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.POs;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class CategoriesViewModel : MvxViewModel
	{
		private readonly ICategoriesService _categoriesService;

		public CategoriesViewModel(ICategoriesService categoriesService)
		{
			_categoriesService = categoriesService;

			SelectCategoryCommand = new MvxCommand<CategoryPO>(SelectCategoryAction);
			ConfirmCategoryCommand = new MvxCommand(ConfirmCategoryAction, AnyCategorySelected);
		}

		public override void Start()
		{
			Categories = _categoriesService.GetCategories();

			foreach (var category in Categories)
			{
				category.SelectCommand = SelectCategoryCommand;
			}
		}

		private IEnumerable<CategoryPO> _categories;
		public IEnumerable<CategoryPO> Categories
		{
			get => _categories;
			set => SetProperty(ref _categories, value);
		}

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

		public IMvxCommand ConfirmCategoryCommand { get; }
		private void ConfirmCategoryAction()
		{
			var selectedCategory = Categories.Single(x => x.Selected);
			ShowViewModel<QuestionViewModel>(new { categoryId = selectedCategory.Id, categoryName = selectedCategory.Name });
		}

		private bool AnyCategorySelected()
			=> Categories.Any(x => x.Selected);
	}
}
