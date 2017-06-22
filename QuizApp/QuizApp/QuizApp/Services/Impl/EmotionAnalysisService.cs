using System;
using System.Threading;
using System.Threading.Tasks;
using QuizApp.Core.DTOs;
using QuizApp.Core.Enums;
using Xamarin.Forms;

namespace QuizApp.Core.Services.Impl
{
	public class EmotionAnalysisService : IEmotionAnalysisService
	{
		private readonly ICameraService _cameraService;
		private readonly IEmotionServiceProxy _emotionServiceProxy;
		private readonly IToastService _toastService;
		private CancellationTokenSource _cancellationTokenSource;
		private const double DelayTimeInS = 10;

		public EmotionAnalysisService(ICameraService cameraService, IEmotionServiceProxy emotionServiceProxy, IToastService toastService)
		{
			_cameraService = cameraService;
			_emotionServiceProxy = emotionServiceProxy;
			_toastService = toastService;
			Enabled = true;
		}

		public async void StartAnalyzing()
		{
			if (_cancellationTokenSource != null || Enabled == false)
				return;

			_cancellationTokenSource = new CancellationTokenSource();
			await Task.Run(async () =>
			{
				try
				{
					while (_cancellationTokenSource != null && _cancellationTokenSource.IsCancellationRequested == false)
					{
						await Task.Delay(TimeSpan.FromSeconds(DelayTimeInS), _cancellationTokenSource.Token);
						using (var photo = await _cameraService.TakePhoto())
						{
							ShowToast("Photo taken!");
							var emotions = await _emotionServiceProxy.PostPhotoAsync(photo, _cancellationTokenSource.Token);
							AnalyzeEmotions(emotions);
						}
					}
				}
				catch (TaskCanceledException)
				{
				}
			}).ConfigureAwait(false);
		}

		private void AnalyzeEmotions(DetectedEmotionsDTO emotions)
		{
			var anger = $"Anger: {emotions.Anger.ToString("F5")}";
			var sadness = $"Sadness: {emotions.Sadness.ToString("F5")}";
			var contempt = $"Contempt: {emotions.Contempt.ToString("F5")}";
			var happiness = $"Happiness: {emotions.Happiness.ToString("F5")}";

			ShowToast($"{anger}\n{sadness}\n{contempt}\n{happiness}");

			var negativeEmotionsSum = emotions.Anger + emotions.Sadness;
			var positiveEmotionsSum = emotions.Contempt + emotions.Happiness;
			if (positiveEmotionsSum > negativeEmotionsSum)
				RaiseDifficulty();
			else
				LowerDifficulty();
		}

		private void RaiseDifficulty()
		{
			if (CurrentDifficulty < QuestionDifficulty.Hard)
				if (CurrentDifficulty < QuestionDifficulty.Hard)
				{
					CurrentDifficulty++;
					Task.Delay(2000).ContinueWith((task) =>
					{
						ShowToast("Question difficulty raised!");
					});
				}
		}

		private void LowerDifficulty()
		{
			if (CurrentDifficulty > QuestionDifficulty.Easy)
			{
				CurrentDifficulty--;
				Task.Delay(2000).ContinueWith((task) =>
				{
					ShowToast("Question difficulty lowered!");
				});
			}
		}

		private void ShowToast(string text)
		{
			Device.BeginInvokeOnMainThread(() => _toastService.ShowToast(text));
		}

		public void StopAnalyzing()
		{
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource = null;
			CurrentDifficulty = QuestionDifficulty.Medium;
		}

		public QuestionDifficulty CurrentDifficulty { get; private set; } = QuestionDifficulty.Medium;
		public bool Enabled { get; set; }
	}
}