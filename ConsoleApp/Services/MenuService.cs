using Shared.Models;
using Shared.Services;

namespace ConsoleApp.Services;

public class MenuService
{
    private readonly ContactService _contactService;

    public MenuService(ContactService contactService)
    {
        _contactService = contactService;
    }

    public void StartMenu()
    {
        while (true)
        {
            DisplayMenuTitle("MAIN MENU");
            Console.WriteLine($"\t{"1.",-4} Add New Contact");
            Console.WriteLine($"\t{"2.",-4} View Contacts");
            Console.WriteLine($"\t{"3.",-4} View Contact Details");
            Console.WriteLine($"\t{"4.",-4} Delete Contact");
            Console.WriteLine($"\t{"0.",-4} Exit Program");
            Console.WriteLine();
            Console.Write("Please enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowAddContactOption();
                        break;
                    case 2:
                        ShowViewContactsOption();
                        break;
                    case 3:
                        ShowViewContactDetailsOption();
                        break;
                    case 4:
                        ShowDeleteContactOption();
                        break;
                    case 0:
                        ShowExitApplicationOption();
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    private void ShowViewContactsOption()
    {
        DisplayMenuTitle("CONTACTS");

        var contacts = _contactService.GetAllContactsFromList();

            if (!contacts.Any())
            {
                Console.WriteLine("No Contacts Found.");
            }
            else
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"\tFirst Name: {contact.FirstName}");
                    Console.WriteLine($"\tLast Name: {contact.LastName}");
                    Console.WriteLine($"\tPhone Number: {contact.Phone}");
                    Console.WriteLine($"\tEmail: <{contact.Email}>");
                    Console.WriteLine($"\tAddress: {contact.Address}");
                    Console.WriteLine();
                }
            }

        DisplayPressAnyKey();
    }

    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to close the program? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
            Environment.Exit(0);
    }

    private void ShowAddContactOption()
    {
        Contact contact = new Contact();
        DisplayMenuTitle("ADD NEW CONTACT");

        Console.Write("First Name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        contact.Phone = Console.ReadLine()!;

        Console.Write("Email: ");
        contact.Email = Console.ReadLine()!.ToLower();

        Console.Write("Address: ");
        contact.Address = Console.ReadLine()!;

        var ok = _contactService.AddContactToList(contact);

        switch (ok)
        {
            case true:
                Console.WriteLine();
                Console.WriteLine("The contact was added successfully.");
                break;
            case false:
                Console.WriteLine();
                Console.WriteLine("Failed when trying to add contact to address book.");
                break;
        }

        DisplayPressAnyKey();
    }

    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"\n\n\t\t************* {title} *************\n\n");
        Console.WriteLine();
    }

    private void ShowViewContactDetailsOption()
    {
        DisplayMenuTitle("CONTACT DETAILS");

        Console.Write("Enter email of the contact: ");
        string email = Console.ReadLine()!.ToLower();

        var foundContact = _contactService.GetContactFromList(email)!;
        Console.WriteLine();

        if (foundContact != null)
        {
            Console.WriteLine($"\tFirst Name: {foundContact.FirstName}");
            Console.WriteLine($"\tLast Name: {foundContact.LastName}");
            Console.WriteLine($"\tPhone Number: {foundContact.Phone}");
            Console.WriteLine($"\tEmail: <{foundContact.Email}>");
            Console.WriteLine($"\tAddress: {foundContact.Address}");
        }
        else
        {
            Console.WriteLine($"Contact with email <{email}> not found.");
        }

        DisplayPressAnyKey();
    }

    private void ShowDeleteContactOption()
    {
        DisplayMenuTitle("DELETE CONTACT");

        Console.Write("Enter the email of the contact you want to delete: ");
        string email = Console.ReadLine()!.ToLower();

        var contact = _contactService.GetContactFromList(email);

        if (contact != null)
        {
            _contactService.RemoveContactFromList(contact);
            Console.WriteLine();
            Console.WriteLine($"Contact with email <{email}> removed successfully.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"No contact with email <{email}> could be found.");
        }

        DisplayPressAnyKey();
    }
}