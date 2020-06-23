using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GroundpolisMobile.ViewModels
{
	public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
	{
		protected INavigationService NavigationService { get; private set; }

		public ReactiveProperty<bool> IsLoading { get; } = new ReactiveProperty<bool>(false);

		public ViewModelBase(INavigationService navigationService)
		{
			NavigationService = navigationService;
		}

		public virtual void Initialize(INavigationParameters parameters)
		{

		}

		public virtual void OnNavigatedFrom(INavigationParameters parameters)
		{

		}

		public virtual void OnNavigatedTo(INavigationParameters parameters)
		{

		}

		public virtual void Destroy()
		{

		}

		public static readonly HttpClient Http = new HttpClient();
	}
}
