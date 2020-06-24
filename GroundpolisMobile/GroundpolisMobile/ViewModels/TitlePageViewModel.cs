using GroundpolisMobile.Views;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroundpolisMobile.ViewModels
{
	public class TitlePageViewModel : ViewModelBase
	{
		public TitlePageViewModel()
		{
			ExploreInstances.Subscribe(() =>
			{
				Root.Navigation.PushModalAsync(new ExploreInstancesPage());
			});
		}

		public ReactiveCommand ExploreInstances { get; } = new ReactiveCommand();
	}
}
