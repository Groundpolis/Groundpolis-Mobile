using Newtonsoft.Json;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GroundpolisMobile.ViewModels
{
	public class ExploreInstancesPageViewModel : ViewModelBase
	{
		public ReactiveCollection<JoinMisskeyInstanceViewModel> Instances { get; }
			= new ReactiveCollection<JoinMisskeyInstanceViewModel>();

		public ReactiveCommand<JoinMisskeyInstanceViewModel> Chosen { get; } = new ReactiveCommand<JoinMisskeyInstanceViewModel>();
		public ReactiveCommand Reload { get; } = new ReactiveCommand();

		public ExploreInstancesPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Chosen.Subscribe((vm) =>
			{
				NavigationService.NavigateAsync("SignInPage", ("instance", vm));
			});

			Reload.Subscribe(() => FetchInstances());
		}

		public override async void OnNavigatedTo(INavigationParameters parameters)
		{
			if (Instances.Count > 0) return;
			await FetchInstances();
		}

		private async Task FetchInstances()
		{
			IsLoading.Value = true;
			Instances.Clear();
			var res = await Http.GetAsync("https://join.misskey.page/instances.json");
			var json = JsonConvert.DeserializeObject<JoinMisskeyInstances>(await res.Content.ReadAsStringAsync());
			Instances.AddRangeOnScheduler(
				json.Instances
					.Where(i => i.Meta != null)
					.Where(i => !i.Meta.DisableRegistration)
					.OrderByDescending(i => i.Value)
					.Select(i => new JoinMisskeyInstanceViewModel(i))
			);
			IsLoading.Value = false;
		}
	}

	public class JoinMisskeyInstanceViewModel : BindableBase
	{
		public ReactiveProperty<string> Url { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<Meta> Meta { get; } = new ReactiveProperty<Meta>(new Meta());

		public JoinMisskeyInstanceViewModel(JoinMisskeyInstance instance)
		{
			if (instance == null) throw new ArgumentNullException(nameof(instance));
			Url.Value = instance.Url;
			Name.Value = string.IsNullOrEmpty(instance.Meta.Name) ? instance.Url : instance.Meta.Name;
			var desc = string.IsNullOrEmpty(instance.Meta.Description) ? "説明はありません。" : instance.Meta.Description;
			Description.Value = desc;
			Meta.Value = instance.Meta;
		}
	}
}
