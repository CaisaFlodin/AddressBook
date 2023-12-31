using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Services;
using Contact = Shared.Models.Contact;

namespace AddressBookMaui.ViewModels;

public partial class ContactEditViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactService _contactService;

    public ContactEditViewModel(ContactService contactService)
    {
        _contactService = contactService;
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private async Task EditContact()
    {
        _contactService.UpdateContactInList(Contact);
        Contact = new();

        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as Contact)!;
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");
    }
}
