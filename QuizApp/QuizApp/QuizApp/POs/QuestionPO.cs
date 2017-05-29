using System.Collections.Generic;

namespace QuizApp.POs
{
	public class QuestionPO
	{
		public string Question { get; set; }
		public IList<string> Answers { get; set; }
	}
}
