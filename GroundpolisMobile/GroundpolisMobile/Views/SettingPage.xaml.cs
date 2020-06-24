using GroundpolisMobile.ViewModels;
using Xamarin.Forms;

namespace GroundpolisMobile.Views
{
	public partial class SettingPage : ContentPage
	{
		public SettingPage()
		{
			InitializeComponent();
			BindingContext = new SettingPageViewModel();
		}
	}
}
