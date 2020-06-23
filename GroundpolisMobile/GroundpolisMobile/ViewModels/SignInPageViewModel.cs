using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GroundpolisMobile.ViewModels
{
	public class SignInPageViewModel : ViewModelBase
	{
		public ReactiveProperty<string> Url { get; } = new ReactiveProperty<string>();
		public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
		public ReactiveProperty<User> User { get; } = new ReactiveProperty<User>();
		public ReactiveProperty<bool> IsBusy { get; } = new ReactiveProperty<bool>(false);
		public ReactiveProperty<string> Error { get; } = new ReactiveProperty<string>();


		public ReadOnlyReactiveProperty<bool> SignInVisible { get; }
		public ReadOnlyReactiveProperty<bool> UserVisible { get; }
		public ReadOnlyReactiveProperty<bool> ErrorVisible { get; }

		public ReactiveCommand SignIn { get; }

		public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			SignInVisible = User.CombineLatest(Error, (v, e) => v == null && e == null).ToReadOnlyReactiveProperty();
			UserVisible = User.Select(v => v != null).ToReadOnlyReactiveProperty();
			ErrorVisible = Error.Select(v => v != null).ToReadOnlyReactiveProperty();

			SignIn = new ReactiveCommand(IsBusy.Select(i => !i));
			SignIn.Subscribe(async () =>
			{
				IsBusy.Value = true;
				var miauthUrl = $"https://{Url.Value}/api/miauth/{uuid}/check";
				var res = await Http.PostAsync(miauthUrl, new StringContent(""));
				var json = await res.Content.ReadAsStringAsync();

				var status = JsonConvert.DeserializeObject<MiAuthStatus>(json);
				if (status.Ok)
				{
					await Groundpolis.SignInAsync(status.Token, Url.Value);
				}
				else
				{
					Error.Value = "認証に失敗しました";
				}
			});
		}

		public override void OnNavigatedTo(INavigationParameters parameters)
		{
			IsBusy.Value = true;
			var vm = parameters["instance"] as JoinMisskeyInstanceViewModel;
			Url.Value = vm.Url.Value;
			Name.Value = vm.Name.Value;
			if (!(vm.Meta.Value.Features?.miauth ?? false))
			{
				Error.Value = "このインスタンスは MiAuth をサポートしないため、Groundpolis Mobile では現在ご利用いただけません。";
				return;
			}

			MiAuthAsync();

			IsBusy.Value = false;
		}

		private async Task MiAuthAsync()
		{

			uuid = Guid.NewGuid().ToString();
			var url = $"https://{Url.Value}/miauth/{uuid}?name=Groundpolis+Mobile&icon={System.Web.HttpUtility.UrlEncode("https://groundpolis.app/assets/icon.svg")}&permission=read:account,write:account,read:blocks,write:blocks,read:drive,write:drive,read:favorites,write:favorites,read:following,write:following,read:messaging,write:messaging,read:mutes,write:mutes,write:notes,read:notifications,write:notifications,read:reactions,write:reactions,write:votes,read:pages,write:pages,write:page-likes,read:page-likes,read:user-groups,write:user-groups";
			// MVVM の道義に反するけど、しらねー
			await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
		}

		private string uuid;
	}
}
