using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GroundpolisMobile.ViewModels
{
	public class ProfilePageViewModel : ViewModelBase
	{
		public ReactiveProperty<User> User { get; } = new ReactiveProperty<User>();

		public ProfilePageViewModel() : this(null) { }

		public ProfilePageViewModel(string userId)
		{
			if (!Groundpolis.IsOnline) return;
			async void Init()
			{
				User.Value = await (userId != null ? Groundpolis.PostAsync<User>("users/show", new Dictionary<string, object>
				{
					{ "userId", userId }
				}) : Groundpolis.IAsync());
			}

			if (userId == null && Groundpolis.CurrentSession?.User != null)
			{
				// キャッシュデータを表示する
				User.Value = Groundpolis.CurrentSession.User;
			}

			// 新鮮なデータを取りに行く
			Init();
		}
	}
}