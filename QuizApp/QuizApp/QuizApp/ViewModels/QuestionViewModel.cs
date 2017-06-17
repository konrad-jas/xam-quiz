using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
		private readonly IScoreAssessorFactory _scoreAssessorFactory;

		private int _categoryId;
		private string _categoryName;

		public QuestionViewModel(IQuestionsService questionsService, IScoreAssessorFactory scoreAssessorFactory)
		{
			_questionsService = questionsService;
			_scoreAssessorFactory = scoreAssessorFactory;
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
		private IScoreAssessor _scoreAssessor;
		private CancellationTokenSource _cancellationTokenSource;
		private double _remainingTime;

		public int Score
		{
			get => _score;
			set => SetProperty(ref _score, value);
		}

		public IMvxCommand ConfirmAnswerCommand { get; }
		private async Task ConfirmAnswerAction()
		{
			_scoreAssessor.StopTimer();
			Cleanup();
			var selectedAnswer = Answers.Single(x => x.Selected);
			if (selectedAnswer.Correct)
			{
				Score += _scoreAssessor.EvaluateScore();

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
			var question = await _questionsService.GetQuestionAsync(_categoryId, QuestionDifficulty.Medium);
			if (question == null)
				return;

			Question = question.Question;
			Answers = question.Answers;

			foreach (var answer in question.Answers)
			{
				answer.SelectedCommand = SelectAnswerCommand;
			}
			Cleanup();
			_scoreAssessor = _scoreAssessorFactory.GetAssessor();
			_scoreAssessor.OnTimeRanOut += OnTimeRanOut;
			_scoreAssessor.StartTimer();
			InitCountdown();
		}

		private async void InitCountdown()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			RemainingTime = 1;
			await Task.Run(UpdateCountdown).ConfigureAwait(false);
		}

		public double RemainingTime
		{
			get => _remainingTime;
			set => SetProperty(ref _remainingTime, value);
		}

		private async Task UpdateCountdown()
		{
			while (_cancellationTokenSource.IsCancellationRequested == false)
			{
				try
				{
					await Task.Delay(TimeSpan.FromSeconds(1), _cancellationTokenSource.Token);
					InvokeOnMainThread(() => RemainingTime = _scoreAssessor.RemainingTime);
				}
				catch (TaskCanceledException)
				{
				}
			}
		}

		private void Cleanup()
		{
			if (_scoreAssessor != null)
			{
				_scoreAssessor.StopTimer();
				_scoreAssessor.OnTimeRanOut -= OnTimeRanOut;
			}
			_cancellationTokenSource?.Cancel();
		}

		private void OnTimeRanOut(object sender, EventArgs e)
		{
			Cleanup();
			ShowViewModel<FinalScoreViewModel>(new { score = Score });
		}
	}
}
