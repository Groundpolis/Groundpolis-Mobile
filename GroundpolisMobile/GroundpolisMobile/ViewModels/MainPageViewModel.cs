using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroundpolisMobile.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public MainPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
		}

		public DelegateCommand ExploreInstances => new DelegateCommand(() =>
		{
			NavigationService.NavigateAsync("ExploreInstancesPage");
		});
	}
}
