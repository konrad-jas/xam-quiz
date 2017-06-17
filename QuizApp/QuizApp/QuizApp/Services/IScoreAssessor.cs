﻿using System;

namespace QuizApp.Core.Services
{
	public interface IScoreAssessor
	{
		void StartTimer();
		event EventHandler OnTimeRanOut;
		double Progress { get; }
		void StopTimer();
		int EvaluateScore();
	}
}
