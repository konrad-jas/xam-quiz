using MvvmCross.Core.ViewModels;

namespace QuizApp.Core.POs
{
	public class AnswerPO : MvxNotifyPropertyChanged
	{
		public string Answer { get; set; }
		public bool Correct { get; set; }
		public bool Selected { get; set; }
		public IMvxCommand SelectedCommand { get; set; }

		bool _highlight;
		public bool Highlight
		{
			get { return _highlight; }
			set { SetProperty(ref _highlight, value); }
		}
	}
}
