﻿using Xamarin.Forms.Xaml;

namespace QuizApp.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NamePage
	{
		public NamePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			NameEntry.Focus();
		}
	}
}