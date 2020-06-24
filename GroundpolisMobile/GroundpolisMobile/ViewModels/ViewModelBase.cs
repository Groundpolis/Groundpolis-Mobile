using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace GroundpolisMobile.ViewModels
{
	public class ViewModelBase
	{
		public ReactiveProperty<bool> IsLoading { get; } = new ReactiveProperty<bool>(false);

		public ReactiveProperty<bool> IsLoaded { get; } = new ReactiveProperty<bool>(false);

		public static readonly HttpClient Http = new HttpClient();

		public Shell Root => Shell.Current;
	}
}
