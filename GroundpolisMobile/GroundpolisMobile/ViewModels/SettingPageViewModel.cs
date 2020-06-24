using GroundpolisMobile.Views;
using Reactive.Bindings;

namespace GroundpolisMobile.ViewModels
{
	public class SettingPageViewModel : ViewModelBase
	{
		public ReactiveCommand SignOut { get; } = new ReactiveCommand();
		public ReactiveCommand AddAccount { get; } = new ReactiveCommand();

		public SettingPageViewModel()
		{
			AddAccount.Subscribe(async () =>
			{
				await Root.Navigation.PushModalAsync(new ExploreInstancesPage());
			});
			SignOut.Subscribe(async () =>
			{
				await Groundpolis.SignOutAsync();

			});
		}
	}
}
