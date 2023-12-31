using AddressBookMaui.ViewModels;

namespace AddressBookMaui.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(ContactListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}