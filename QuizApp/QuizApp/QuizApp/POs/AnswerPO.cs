using MvvmCross.Core.ViewModels;

namespace QuizApp.Core.POs
{
	public class AnswerPO
	{
		public string Answer { get; set; }
		public bool Correct { get; set; }
		public bool Selected { get; set; }
		public IMvxCommand SelectedCommand { get; set; }
	}
}
