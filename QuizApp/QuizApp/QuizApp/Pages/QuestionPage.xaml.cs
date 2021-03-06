﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizApp.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestionPage
	{
		public QuestionPage()
		{
			InitializeComponent();

			NavigationPage.SetHasBackButton(this, false);
		}

		protected override bool OnBackButtonPressed()
			=> true;
	}
}