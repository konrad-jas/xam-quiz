using System;
using System.Threading;
using System.Threading.Tasks;
using QuizApp.Core.DTOs;
using QuizApp.Core.Enums;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionAnalysisService : IEmotionAnalysisService
	{
		private readonly ICameraService _cameraService;
		private readonly IEmotionServiceProxy _emotionServiceProxy;
		private readonly IToastService _toastService;
		private CancellationTokenSource _cancellationTokenSource;
		private const double DelayTimeInS = 60;

		public EmotionAnalysisService(ICameraService cameraService, IEmotionServiceProxy emotionServiceProxy, IToastService toastService)
		{
			_cameraService = cameraService;
			_emotionServiceProxy = emotionServiceProxy;
			_toastService = toastService;
		}

		public async void StartAnalyzing()
		{
			if(_cancellationTokenSource != null)
				return;

			_cancellationTokenSource = new CancellationTokenSource();
			await Task.Run(async () =>
			{
				try
				{
					var photo = await _cameraService.TakePhoto();
					var emotions = await _emotionServiceProxy.PostPhotoAsync(photo, _cancellationTokenSource.Token);
					AnalyzeEmotions(emotions);
					await Task.Delay(TimeSpan.FromSeconds(DelayTimeInS), _cancellationTokenSource.Token);
				}
				catch (TaskCanceledException)
				{
				}
			});
		}

		private void AnalyzeEmotions(DetectedEmotionsDTO emotions)
		{
			var negativeEmotionsSum = emotions.Anger + emotions.Sadness;
			var positiveEmotionsSum = emotions.Contempt + emotions.Happiness;
			if (positiveEmotionsSum > negativeEmotionsSum)
				LowerDifficulty();
			else
				RaiseDifficulty();
		}

		private void RaiseDifficulty()
		{
			if (CurrentDifficulty < QuestionDifficulty.Hard)
			{
				CurrentDifficulty++;
				_toastService.ShowToast("Question difficulty raised!");
			}
		}

		private void LowerDifficulty()
		{
			if (CurrentDifficulty > QuestionDifficulty.Easy)
			{
				CurrentDifficulty--;
				_toastService.ShowToast("Question difficulty lowered!");
			}
		}

		public void StopAnalyzing()
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource = null;
			CurrentDifficulty = QuestionDifficulty.Medium;
		}

		public QuestionDifficulty CurrentDifficulty { get; private set; } = QuestionDifficulty.Medium;
	}
}