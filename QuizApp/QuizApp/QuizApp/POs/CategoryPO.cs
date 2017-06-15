using MvvmCross.Core.ViewModels;

namespace QuizApp.Core.POs
{
	public class CategoryPO
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public bool Selected { get; set; }
		public IMvxCommand SelectCommand { get; set; }
	}
}
