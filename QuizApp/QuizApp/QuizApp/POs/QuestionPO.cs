using System.Collections.Generic;

namespace QuizApp.Core.POs
{
	public class QuestionPO
	{
		public string Question { get; set; }
		public IList<AnswerPO> Answers { get; set; }
	}
}
