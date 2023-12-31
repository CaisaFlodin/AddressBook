using AddressBookMaui.ViewModels;

namespace AddressBookMaui.Pages;

public partial class ContactEditPage : ContentPage
{
	public ContactEditPage(ContactEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}