using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface IHighscoresService
	{
		Task<IEnumerable<ScorePO>> GetHighscoresAsync();
		Task<(bool InHighscores, int Place)> QualifiesForHighscores(int result);
		Task AddHighscoreAsync(int result, string user);
	}
}
