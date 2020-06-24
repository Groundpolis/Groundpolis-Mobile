using GroundpolisMobile.ViewModels;
using Xamarin.Forms;

namespace GroundpolisMobile.Views
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new MainPageViewModel();
		}
	}
}
