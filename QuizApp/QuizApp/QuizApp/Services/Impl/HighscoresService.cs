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
			for (var i = 0; i < Places; ++i)
			{
				var place = i + 1;
				if (mappedScores.Count > i)
					mappedScores[i].Place = place;
				else
					mappedScores.Add(new ScorePO { Place = place, Result = "-", User = "-"});
			}
			return mappedScores;
		}

		public async Task<(bool InHighscores, int Place)> QualifiesForHighscores(int result)
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
