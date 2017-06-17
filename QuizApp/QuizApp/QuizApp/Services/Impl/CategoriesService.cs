using System.Collections.Generic;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services.Impl
{
	public class CategoriesService : ICategoriesService
	{
		public IEnumerable<CategoryPO> GetCategories()
		{
			var categories = new[]
			{
				new CategoryPO
				{
					Id = 9,
					Name = "General Knowledge"
				},
				new CategoryPO
				{
					Id = 10,
					Name = "Entertainment: Books"
				},
				new CategoryPO
				{
					Id = 11,
					Name = "Entertainment: Film"
				},
				new CategoryPO
				{
					Id = 12,
					Name = "Entertainment: Music"
				},
				new CategoryPO
				{
					Id = 14,
					Name = "Entertainment: Television"
				},
				new CategoryPO
				{
					Id = 15,
					Name = "Entertainment: Video Games"
				},
				new CategoryPO
				{
					Id = 17,
					Name = "Science & Nature"
				},
				new CategoryPO
				{
					Id = 18,
					Name = "Science: Computers"
				},
				new CategoryPO
				{
					Id = 19,
					Name = "Science: Mathematics"
				},
				new CategoryPO
				{
					Id = 20,
					Name = "Mythology"
				},
				new CategoryPO
				{
					Id = 21,
					Name = "Sports"
				},
				new CategoryPO
				{
					Id = 22,
					Name = "Geography"
				},
				new CategoryPO
				{
					Id = 23,
					Name = "History"
				},
				new CategoryPO
				{
					Id = 26,
					Name = "Celebrities"
				},
				new CategoryPO
				{
					Id = 27,
					Name = "Animals"
				}
			};

			return categories;
		}
	}
}
