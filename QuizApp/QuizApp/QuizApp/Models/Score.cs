using Realms;

namespace QuizApp.Core.Models
{
	public class Score : RealmObject
	{
		public int Result { get; set; }
		public string User { get; set; }
		public string Category { get; set; }
	}
}
