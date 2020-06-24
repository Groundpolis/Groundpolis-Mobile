using GroundpolisMobile.Views;
using Newtonsoft.Json;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace GroundpolisMobile.ViewModels
{
	public class ExploreInstancesPageViewModel : ViewModelBase
	{
		public ReactiveCollection<JoinMisskeyInstanceViewModel> Instances { get; }
			= new ReactiveCollection<JoinMisskeyInstanceViewModel>();

		public ReactiveCommand<JoinMisskeyInstanceViewModel> Chosen { get; } = new ReactiveCommand<JoinMisskeyInstanceViewModel>();
		public ReactiveCommand Close { get; } = new ReactiveCommand();
		public ReactiveCommand Reload { get; } = new ReactiveCommand();

		public ExploreInstancesPageViewModel()
		{
			Chosen.Subscribe((vm) =>
			{
				SignInAsync(vm);
			});

			Close.Subscribe(() => Root.Navigation.PopModalAsync());

			Reload.Subscribe(() => FetchInstances());
			FetchInstances();
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
					.OrderByDescending(i => i.Meta.IsGroundpolis ? 1 : 0)
					.Select(i => new JoinMisskeyInstanceViewModel(i))
			);

			IsLoading.Value = false;
		}

		private async Task SignInAsync(JoinMisskeyInstanceViewModel vm)
		{
			if (!vm.Meta.Value.Features.miauth)
			{
				await Root.DisplayAlert("ログインできません", "Groundpolis Mobile は MiAuth 認証のみを現在サポートしていますが、このインスタンスは旧式の認証方式のみをサポートしているため、ご利用いただけません。他のインスタンスをご利用ください", "OK");
				return;
			}

			await MiAuthAsync(vm);
		}



		private async Task MiAuthAsync(JoinMisskeyInstanceViewModel vm)
		{
			var uuid = Guid.NewGuid().ToString();

			var url = $"https://{vm.Url.Value}/miauth/{uuid}?"
				+ "name=Groundpolis+Mobile"
				+ $"&callback={HttpUtility.UrlEncode(Const.MIAUTH_CALLBACK)}"
				+ $"&permission={string.Join(",", Groundpolis.Permission)}";

			// MVVM の流儀に反するけど、しらねー
			try
			{
				await WebAuthenticator.AuthenticateAsync(new Uri(url), new Uri(Const.MIAUTH_CALLBACK));
			}
			catch (Exception)
			{
				return;
			}

			var miauthUrl = $"https://{vm.Url.Value}/api/miauth/{uuid}/check";
			var res = await Http.PostAsync(miauthUrl, new StringContent(""));
			var json = await res.Content.ReadAsStringAsync();

			var status = JsonConvert.DeserializeObject<MiAuthStatus>(json);
			if (status.Ok)
			{
				await Groundpolis.SignInAsync(status.Token, vm.Url.Value);
				while (Root.Navigation.ModalStack.Count > 0)
					await Root.Navigation.PopModalAsync();
			}
			else
			{
				await Root.DisplayAlert("認証に失敗しました", "もう一度やり直してください。", "OK");
			}
		}
	}
}
