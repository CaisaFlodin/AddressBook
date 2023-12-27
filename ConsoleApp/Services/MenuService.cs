using Shared.Interfaces;
using Shared.Models;

namespace ConsoleApp.Services;

public class MenuService
{
    private readonly IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void StartMenu()
    {
        bool repeat = true;

        do
        {
            DisplayMenu();
            Console.Write("\nYour choice: ");
            //int choice = int.Parse(Console.ReadLine()!);
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting program");
                        repeat = false;
                        break;
                    case 1:
                        ShowAddContactOption();
                        break;
                    case 2:
                        ShowContactInfoOption();
                        break;
                    case 3:
                        ShowAllContactsOption();
                        break;
                    case 4:
                        ShowUpdateContactOption();
                        break;
                    case 5:
                        ShowDeleteContactOption();
                        break;
                }
            }
            else
                Console.WriteLine("Whoops that's not a number. Try again!");
        }
        while (repeat);
    }

    public void ShowUpdateContactOption()
    {
        Console.Write("Enter the email of the contact you want to update: ");
        string email = Console.ReadLine()!;
        if (_contactService.CheckIfEmailExists(email))
        {
            var updatedContact = GetUpdatedContact();
            _contactService.UpdateContactInList(email, updatedContact);


            Console.WriteLine($"Contact {updatedContact.FirstName} {updatedContact.FirstName} updated successfully.");
        }
        else
        {
            Console.WriteLine($"No contact with email <{email}> could be found. Press any key to continue.");
            Console.ReadKey();
        }
    }
    public IContact GetUpdatedContact()
    {
        Console.Write("Enter new first name: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Enter new last name: ");
        string lastName = Console.ReadLine()!;
        Console.Write("Enter new phone number: ");
        string phoneNumber = Console.ReadLine()!;
        Console.Write("Enter new email: ");
        string updatedEmail = Console.ReadLine()!;
        Console.Write("Enter new address: ");
        string address = Console.ReadLine()!;
        Console.WriteLine();

        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = updatedEmail,
            Address = address
        };
        return contact;
    }

    public void ShowDeleteContactOption()
    {
        Console.Write("Enter the email of the contact you want to delete: ");
        string email = Console.ReadLine()!;

        var contact = _contactService.RemoveContactFromList(email);
        if (!contact)
        {
            Console.WriteLine("Contact not found. Press any key to continue.");
            Console.ReadKey();
        }
        else
        {
            _contactService.RemoveContactFromList(email);
            Console.WriteLine($"Contact with email {email} removed successfully.");
        }
    }

    public void ShowContactInfoOption()
    {
        Console.Write("Enter the email of the contact: ");
        string email = Console.ReadLine()!;

        var foundContact = _contactService.GetContactFromList(email)!;

        if (foundContact is IContact contact)
        {
            Console.WriteLine(contact.FirstName);
            Console.WriteLine(contact.LastName);
            Console.WriteLine(contact.PhoneNumber);
            Console.WriteLine(contact.Email);
            Console.WriteLine(contact.Address);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Contact with email <{email}> not found. Press any key to continue.");
            Console.ReadKey();
        }
    }

    public void DisplayMenu()
    {
        string[] menuOptions = ["Exit Program", "Add Contact", "Get Contact", "Get Contacts", "Update Contact", "Delete Contact"];

        for (int i = 0; i < menuOptions.Length; i++)
        {
            Console.WriteLine($"{i}. {menuOptions[i],-3}");
        }
    }

    public void ShowAddContactOption()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine()!;
        Console.Write("Enter phone number: ");
        string phoneNumber = Console.ReadLine()!;
        Console.Write("Enter email: ");
        string email = Console.ReadLine()!;
        Console.Write("Enter address: ");
        string address = Console.ReadLine()!;
        Console.WriteLine();

        _contactService.AddContactToList(new Contact()
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            Address = address
        });
    }

    public void ShowAllContactsOption()
    {
        var contacts = _contactService.GetAllContactsFromList();

        if (contacts is List<IContact> list && list.Count > 0)
        {
            foreach (var contact in list)
            {
                Console.WriteLine(contact.FirstName);
                Console.WriteLine(contact.LastName);
                Console.WriteLine(contact.PhoneNumber);
                Console.WriteLine(contact.Email);
                Console.WriteLine(contact.Address);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Your address book is empty. Press any key to continue.");
            Console.ReadKey();
        }
    }
}
