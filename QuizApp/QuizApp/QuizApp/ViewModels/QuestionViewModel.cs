using System.Collections.Generic;
using System.Linq;
using QuizApp.Core.Services;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.Enums;
using QuizApp.Core.POs;

namespace QuizApp.Core.ViewModels
{
	public class QuestionViewModel : MvxViewModel
	{
		private readonly IQuestionsService _questionsService;

		private int _categoryId;
		private string _categoryName;

		public QuestionViewModel(IQuestionsService questionsService)
		{
			_questionsService = questionsService;

			SelectAnswerCommand = new MvxCommand<AnswerPO>(SelectAnswerAction);
			ConfirmAnswerCommand = new MvxAsyncCommand(ConfirmAnswerAction, AnyAnswerSelected);

			Answers = new List<AnswerPO>();
		}

		public void Init(int categoryId, string categoryName)
		{
			_categoryId = categoryId;
			_categoryName = categoryName;
		}

		public override async void Start()
		{
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
				ShowViewModel<FinalScoreViewModel>(new { score = Score });
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
