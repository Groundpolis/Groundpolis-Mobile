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
			// �Ƃ肠������ View ��U���Ă���
			MainPage = new ContentPage();

			await Groundpolis.InitializeAsync();

			// Groundpolis �N���C�A���g�̏��������ς񂾂̂ŃV�F����\��
			MainPage = new AppShell();

			Groundpolis.CurrentSessionState.Subscribe(SummonModalIfNeeded);
		}

		private void SummonModalIfNeeded(Session s)
		{
			if (s == null)
			{
				// �Z�b�V���������Ȃ̂Ń��[�_�����o���ăT�C���C���𑣂�
				Shell.Current.Navigation.PushModalAsync(new TitlePage());
			}
		}
	}
}