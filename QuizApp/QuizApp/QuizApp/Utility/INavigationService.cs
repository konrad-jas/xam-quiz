using System.Threading.Tasks;
using QuizApp.ViewModels.Base;

namespace QuizApp.Utility
{
	public interface INavigationService
	{
		Task ShowViewModel<TViewModel>() where TViewModel : BaseViewModel;
	}
}
