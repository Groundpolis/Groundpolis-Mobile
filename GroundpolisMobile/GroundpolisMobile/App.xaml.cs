using GroundpolisMobile.ViewModels;
using GroundpolisMobile.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GroundpolisMobile
{
	public partial class App
	{
		public App()
		{
			InitializeComponent();
			Bootstrap();
		}

		private async void Bootstrap()
		{
			// とりあえず空 View を振っておく
			MainPage = new ContentPage();

			await Groundpolis.InitializeAsync();

			// Groundpolis クライアントの初期化が済んだのでシェルを表示
			MainPage = new AppShell();

			Groundpolis.CurrentSessionState.Subscribe(SummonModalIfNeeded);
		}

		private void SummonModalIfNeeded(Session s)
		{
			if (s == null)
			{
				// セッションが無なのでモーダルを出してサインインを促す
				Shell.Current.Navigation.PushModalAsync(new TitlePage());
			}
		}
	}
}