using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using QuizApp.NotificationObjects;
using QuizApp.Utility;
using QuizApp.ViewModels.Base;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
	public class QuestionViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly Command _confirmAnswearCommand;

		public QuestionViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			Question = "To be, or not to be?";
			_confirmAnswearCommand = new Command(ConfirmAnswearAction, AnyAnswearSelected);
			SelectAnswearCommand = new Command<AnswearNO>(SelectAnswearAction);
			Answears = new[]
			{
				new AnswearNO {Answer = "first", SelectedCommand = SelectAnswearCommand},
				new AnswearNO {Answer = "second", SelectedCommand = SelectAnswearCommand},
				new AnswearNO {Answer = "third", SelectedCommand = SelectAnswearCommand},
				new AnswearNO {Answer = "fourth", SelectedCommand = SelectAnswearCommand}
			};
		}

		public string Question { get; set; }

		public IEnumerable<AnswearNO> Answears { get; set; }

		public ICommand ConfirmAnswearCommand => _confirmAnswearCommand;
		private async void ConfirmAnswearAction()
		{
			await _navigationService.ShowViewModel<QuestionViewModel>();
		}

		private bool AnyAnswearSelected()
		{
			return Answears.Any(answear => answear.Selected);
		}

		public ICommand SelectAnswearCommand { get; private set; }

		private void SelectAnswearAction(AnswearNO selectedAnswear)
		{
			if(selectedAnswear.Selected)
				return;

			foreach (var answear in Answears)
			{
				answear.Selected = false;
			}
			selectedAnswear.Selected = true;
			_confirmAnswearCommand.ChangeCanExecute();
		}
	}
}
