using System.Windows.Input;

namespace QuizApp.NotificationObjects
{
	public class AnswearNO : NotificationObject
	{
		public string Answer { get; set; }
		public bool Selected { get; set; }
		public ICommand SelectedCommand { get; set; }
	}
}
