using System.Collections.Generic;
using System.Linq;
using QuizApp.Core.Services;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.POs;

namespace QuizApp.Core.ViewModels
{
	public class QuestionViewModel : MvxViewModel
	{
		private readonly IQuestionsService _questionsService;

		public QuestionViewModel(IQuestionsService questionsService)
		{
			_questionsService = questionsService;

			SelectAnswerCommand = new MvxCommand<AnswerPO>(SelectAnswerAction);
			ConfirmAnswerCommand = new MvxCommand(ConfirmAnswerAction, AnyAnswerSelected);

			Answers = new List<AnswerPO>();
		}

		public override async void Start()
		{
			await LoadQuestion();
		}

		private string _question;
		public string Question
		{
			get { return _question; }
			set
			{
				SetProperty(ref _question, value, "Question");
			}
		}

		private IEnumerable<AnswerPO> _answers;
		public IEnumerable<AnswerPO> Answers
		{
			get { return _answers; }
			set
			{
				SetProperty(ref _answers, value, "Answers");
			}
		}

		public IMvxCommand ConfirmAnswerCommand { get; }
		private async void ConfirmAnswerAction()
		{
			await LoadQuestion();
			ConfirmAnswerCommand.RaiseCanExecuteChanged();
		}

		private bool AnyAnswerSelected()
		{
			return Answers.Any(answear => answear.Selected);
		}

		public IMvxCommand SelectAnswerCommand { get; }

		private void SelectAnswerAction(AnswerPO selectedAnswear)
		{
			if (selectedAnswear.Selected)
				return;

			foreach (var answear in Answers)
			{
				answear.Selected = false;
			}

			selectedAnswear.Selected = true;
			ConfirmAnswerCommand.RaiseCanExecuteChanged();
		}

		private async Task LoadQuestion()
		{
			var question = await _questionsService.Get();
			if (question == null)
				return;

			Question = question.Question;
			Answers = question.Answers.Select(x => new AnswerPO
			{
				Answer = x,
				SelectedCommand = SelectAnswerCommand
			}).ToList();
		}
	}
}
