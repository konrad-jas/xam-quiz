using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.NavObjects;

namespace QuizApp.Core.ViewModels
{
	public class FinalScoreViewModel : MvxViewModel<FinalScoreNavObject>
	{
		private int _score;
		public int Score
		{
			get => _score;
			set => SetProperty(ref _score, value);
		}

		public override Task Initialize(FinalScoreNavObject parameter)
		{
			Score = parameter.Score;

			return Task.FromResult(true);
		}
	}
}
