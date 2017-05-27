using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace QuizApp.ViewModels.Base
{
	public class NotificationObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var memberExpr = propertyExpression.Body as MemberExpression;
			var memberName = memberExpr?.Member.Name;
			if (memberName != null)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}