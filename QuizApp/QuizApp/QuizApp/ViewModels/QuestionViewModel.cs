using System.Collections.Generic;
using System.Linq;
using QuizApp.Core.Services;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.Enums;
using QuizApp.Core.POs;
using MvvmCross.Core.Navigation;
using QuizApp.Core.NavObjects;

namespace QuizApp.Core.ViewModels
{
	public class QuestionViewModel : MvxViewModel<QuestionNavObject>
	{
		private readonly IQuestionsService _questionsService;
		private readonly IMvxNavigationService _navigationService;

		private int _categoryId;
		private string _categoryName;

		public QuestionViewModel(IQuestionsService questionsService, IMvxNavigationService navigationService)
		{
			_questionsService = questionsService;
			_navigationService = navigationService;

			SelectAnswerCommand = new MvxCommand<AnswerPO>(SelectAnswerAction);
			ConfirmAnswerCommand = new MvxAsyncCommand(ConfirmAnswerAction, AnyAnswerSelected);

			Answers = new List<AnswerPO>();
		}

		public override async Task Initialize(QuestionNavObject parameter)
		{
			_categoryId = parameter.CategoryId;
			_categoryName = parameter.CategoryName;

			await LoadQuestion();
		}

		private string _question;
		public string Question
		{
			get => _question;
			set => SetProperty(ref _question, value);
		}

		private IEnumerable<AnswerPO> _answers;
		public IEnumerable<AnswerPO> Answers
		{
			get => _answers;
			set => SetProperty(ref _answers, value);
		}

		private int _score;
		public int Score
		{
			get => _score;
			set => SetProperty(ref _score, value);
		}

		public IMvxCommand ConfirmAnswerCommand { get; }
		private async Task ConfirmAnswerAction()
		{
			var selectedAnswer = Answers.Single(x => x.Selected);
			if (selectedAnswer.Correct)
			{
				Score++;

				await LoadQuestion();
				ConfirmAnswerCommand.RaiseCanExecuteChanged();
			}
			else
			{
				var finalScoreNavObject = new FinalScoreNavObject { Score = Score };
				await _navigationService.Navigate<FinalScoreViewModel, FinalScoreNavObject>(finalScoreNavObject);
			}
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

			foreach (var answer in Answers)
			{
				answer.Selected = false;
			}

			selectedAnswear.Selected = true;
			ConfirmAnswerCommand.RaiseCanExecuteChanged();
		}

		private async Task LoadQuestion()
		{
			var question = await _questionsService.GetQuestion(_categoryId, QuestionDifficulty.Medium);
			if (question == null)
				return;

			Question = question.Question;
			Answers = question.Answers;

			foreach (var answer in question.Answers)
			{
				answer.SelectedCommand = SelectAnswerCommand;
			}
		}
	}
}
