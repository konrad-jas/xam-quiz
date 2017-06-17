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
			return Mapper.Map<IEnumerable<ScorePO>>(scores);
		}

		public async Task<(bool, int)> MakesIntoHighscoreAsync(int result)
		{
			var scores = await GetTopScores();
			var place = scores.Count(x => result < x.Result) + 1;
			return (place <= Places, place);
		}

		public async Task AddHighscoreAsync(int result, string user, string category)
		{
			var realm = await Realm.GetInstanceAsync(RealmConfiguration.DefaultConfiguration);
			await realm.WriteAsync(tempRealm =>
			{
				tempRealm.Add(new Score
				{
					Result = result,
					User = user,
					Category = category
				});
			});
		}

		private async Task<IEnumerable<Score>> GetTopScores()
		{
			var realm = await Realm.GetInstanceAsync(RealmConfiguration.DefaultConfiguration);
			return await Task.Run(() => realm.All<Score>().OrderByDescending(x => x.Result).Take(Places).ToList());
		}
	}
}
