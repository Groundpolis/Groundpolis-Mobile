using GroundpolisMobile.ViewModels;
using Xamarin.Forms;

namespace GroundpolisMobile.Views
{
	public partial class ExploreInstancesPage : ContentPage
	{
		public ExploreInstancesPage()
		{
			InitializeComponent();
			BindingContext = new ExploreInstancesPageViewModel();
		}
	}
}
