﻿using System.Threading.Tasks;

namespace QuizApp.Core.Services
{
	public interface ITokenService
	{
		Task<string> GetOrCreateTokenAsync();
		Task ResetTokenAsync();
	}
}
