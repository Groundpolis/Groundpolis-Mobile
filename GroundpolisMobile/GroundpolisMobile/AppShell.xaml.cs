using GroundpolisMobile.ViewModels;
using GroundpolisMobile.Views;
using Xamarin.Forms;

namespace GroundpolisMobile
{
	public partial class AppShell
	{
		public AppShell()
		{
			InitializeComponent();
			RegisterRoute();
			BindingContext = new ShellViewModel();
		}

		public void RegisterRoute()
		{
			Routing.RegisterRoute("main", typeof(TitlePage));
			Routing.RegisterRoute("profile", typeof(ProfilePage));
		}
	}
}