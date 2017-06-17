using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface IHighscoresService
	{
		Task<IEnumerable<ScorePO>> GetHighscoresAsync();
		Task<(bool, int)> MakesIntoHighscoreAsync(int result);
		Task AddHighscoreAsync(int result, string user, string category);
	}
}
