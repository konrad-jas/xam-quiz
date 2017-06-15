using System.Collections.Generic;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface ICategoriesService
	{
		IEnumerable<CategoryPO> GetCategories();
	}
}
