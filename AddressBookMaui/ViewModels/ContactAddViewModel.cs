using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Services;
using Contact = Shared.Models.Contact;

namespace AddressBookMaui.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly ContactService _contactService;

    public ContactAddViewModel(ContactService contactService)
    {
        _contactService = contactService;
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private async Task AddContact()
    {
        _contactService.AddContactToList(Contact);
        Contact = new();

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");
    }
}
