using AddressBookMaui.ViewModels;

namespace AddressBookMaui.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(ContactAddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}