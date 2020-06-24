using GroundpolisMobile.Views;
using Reactive.Bindings;
using System;
using System.Reactive.Linq;

namespace GroundpolisMobile.ViewModels
{
	public class ShellViewModel : ViewModelBase
	{
		public ReadOnlyReactiveProperty<User> User { get; }

		public ShellViewModel()
		{
			User = Groundpolis.CurrentSessionState.Select(s => s.User).ToReadOnlyReactiveProperty();
		}
	}
}
