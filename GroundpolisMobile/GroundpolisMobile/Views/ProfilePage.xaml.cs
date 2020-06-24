using GroundpolisMobile.ViewModels;
using Xamarin.Forms;

namespace GroundpolisMobile.Views
{
	[QueryProperty("UserId", "userId")]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();
			BindingContext = new ProfilePageViewModel();
		}

		public string UserId
		{
			set => BindingContext = new ProfilePageViewModel(value);
		}
	}
}
