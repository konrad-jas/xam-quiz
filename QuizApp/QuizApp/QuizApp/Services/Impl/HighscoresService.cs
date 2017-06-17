using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizApp.Core.Models;
using QuizApp.Core.POs;
using Realms;

namespace QuizApp.Core.Services.Impl
{
	public class HighscoresService : IHighscoresService
	{
		private const int Places = 5;

		public async Task<IEnumerable<ScorePO>> GetHighscoresAsync()
		{
			var scores = await GetTopScores();
			var mappedScores = Mapper.Map<IList<ScorePO>>(scores);
			for (var i = 0; i < mappedScores.Count; ++i)
			{
				mappedScores[i].Place = i + 1;
			}
			return mappedScores;
		}

		public async Task<(bool, int)> MakesIntoHighscoreAsync(int result)
		{
			var scores = await GetTopScores();
			var place = scores.Count(x => result < x.Result) + 1;
			return (place <= Places, place);
		}

		public async Task AddHighscoreAsync(int result, string user)
		{
			var realm = await Realm.GetInstanceAsync(RealmConfiguration.DefaultConfiguration);
			await realm.WriteAsync(tempRealm =>
			{
				tempRealm.Add(new Score
				{
					Result = result,
					User = user
				});
			});
		}

		private async Task<IEnumerable<Score>> GetTopScores()
		{
			var realm = await Realm.GetInstanceAsync(RealmConfiguration.DefaultConfiguration);
			return realm.All<Score>().OrderByDescending(x => x.Result).ToList().Take(Places);
		}
	}
}
