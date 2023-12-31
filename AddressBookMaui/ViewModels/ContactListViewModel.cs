using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Services;
using System.Collections.ObjectModel;
using Contact = Shared.Models.Contact;

namespace AddressBookMaui.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly ContactService _contactService;

    public ContactListViewModel(ContactService contactService)
    {
        _contactService = contactService;
        Contacts = new ObservableCollection<Contact>(_contactService.GetAllContactsFromList());
        _contactService.ContactsUpdated += (sender, e) =>
        {
            Contacts = new ObservableCollection<Contact>(_contactService.GetAllContactsFromList());
        };
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    [RelayCommand]
    private async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    [RelayCommand]
    private async Task NavigateToEdit(Contact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };

        await Shell.Current.GoToAsync("ContactEditPage", parameters);
    }

    [RelayCommand]
    private void Remove(Contact contact)
    {
        _contactService.RemoveContactFromList(contact);

        Contacts = new ObservableCollection<Contact>(_contactService.GetAllContactsFromList());
    }
}
