using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizApp.Core.Services.Impl
{
	public class ScoreAssessor : IScoreAssessor
	{
		private readonly IScoreAssessorConfig _config;
		private readonly CancellationTokenSource _cancellationTokenSource;
		private DateTime _startTime;
		private int _finalTime;
		private bool _finished;

		public ScoreAssessor(IScoreAssessorConfig config)
		{
			_config = config;
			_cancellationTokenSource = new CancellationTokenSource();
		}

		public async void StartTimer()
		{
			await Task.Run(RunTimer).ConfigureAwait(false);
		}

		private async Task RunTimer()
		{
			_startTime = DateTime.Now;
			try
			{
				await Task.Delay(TimeSpan.FromSeconds(_config.MaxTime), _cancellationTokenSource.Token);
				OnTimeRanOut?.Invoke(this, EventArgs.Empty);
			}
			catch (TaskCanceledException ex)
			{
			}
		}

		public event EventHandler OnTimeRanOut;

		private int GetRemainingSeconds()
		{
			return _config.MaxTime - (DateTime.Now - _startTime).Seconds;
		}

		public double RemainingTime => Math.Round((double)GetRemainingSeconds() / _config.MaxTime, 1);

		public void StopTimer()
		{
			_finalTime = GetRemainingSeconds();
			_finished = true;
			_cancellationTokenSource.Cancel();
		}

		public int EvaluateScore()
		{
			if (_finished == false)
				return 0;

			return _config.BaseReward + _finalTime * _config.RemainingTimeMultiplier;
		}
	}
}