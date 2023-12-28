using Shared.Interfaces;
using Shared.Models;
using System;

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
        while (true)
        {
            DisplayMenuTitle("MAIN MENU");
            Console.WriteLine($"\t{"1.",-4} Add New Contact");
            Console.WriteLine($"\t{"2.",-4} View Contacts");
            Console.WriteLine($"\t{"3.",-4} View Contact Details");
            Console.WriteLine($"\t{"4.",-4} Update Contact");
            Console.WriteLine($"\t{"5.",-4} Delete Contact");
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
                        ShowUpdateContactOption();
                        break;
                    case 5:
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
            else
            {
                Console.WriteLine("Whoops that's not a number. Try again!");
            }
        }
    }

    //private void ShowViewContactsOption()
    //{
    //    DisplayMenuTitle("Contacts");
    //    var res = _contactService.GetAllContactsFromList();

    //    if (res.Status == Enums.ServiceStatus.OK)
    //    {
    //        if (res.Result is List<IContact> contactList)
    //        {
    //            if (!contactList.Any())
    //            {
    //                Console.WriteLine("No Contacts Found.");
    //            }
    //            else
    //            {
    //                foreach (var contact in contactList)
    //                {
    //                    Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
    //                }
    //            }
    //        }
    //    }

    private void ShowViewContactsOption()
    {
        DisplayMenuTitle("CONTACTS");

        var contacts = _contactService.GetAllContactsFromList();

        if (contacts is List<IContact>)
        {
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
                    Console.WriteLine($"\tPhone Number: {contact.PhoneNumber}");
                    Console.WriteLine($"\tEmail: <{contact.Email}>");
                    Console.WriteLine($"\tAddress: {contact.Address}");
                    Console.WriteLine();
                }
            }
        }
        //else
        //{
        //    Console.WriteLine("Your address book is empty. Press any key to continue.");
        //    Console.ReadKey();
        //}
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
        IContact contact = new Contact();
        DisplayMenuTitle("ADD NEW CONTACT");

        Console.Write("First Name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        contact.PhoneNumber = Console.ReadLine()!;

        Console.Write("Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Address: ");
        contact.Address = Console.ReadLine()!;

        var ok = _contactService.AddContactToList(contact);

        switch (ok)
        {
            case true:
                Console.WriteLine();
                Console.WriteLine("The contact was added successfully.");
                break;
            //case Enums.ServiceStatus.ALREADY_EXISTS:
            //    Console.WriteLine("The contact already exists.");
            //    break;
            case false:
                Console.WriteLine();
                Console.WriteLine("Failed when tried adding contact to address book.");
                break;
        }

        DisplayPressAnyKey();
    }

    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        //Console.WriteLine("**************************");
        Console.WriteLine($"\n\n\t\t************* {title} *************\n\n");
        //Console.WriteLine("**************************");
        Console.WriteLine();
    }

    private void ShowViewContactDetailsOption()
    {
        DisplayMenuTitle("CONTACT DETAILS");

        Console.Write("Enter the email of the contact: ");
        string email = Console.ReadLine()!;

        var foundContact = _contactService.GetContactFromList(email)!;
        Console.WriteLine();
        if (foundContact is IContact contact)
        {
            Console.WriteLine($"\tFirst Name: {contact.FirstName}");
            Console.WriteLine($"\tLast Name: {contact.LastName}");
            Console.WriteLine($"\tPhone Number: {contact.PhoneNumber}");
            Console.WriteLine($"\tEmail: <{contact.Email}>");
            Console.WriteLine($"\tAddress: {contact.Address}");
        }
        else
        {
            Console.WriteLine($"Contact with email <{email}> not found.");
        }

        DisplayPressAnyKey();
    }

    public void ShowUpdateContactOption()
    {
        DisplayMenuTitle("UPDATE CONTACT");

        Console.Write("Enter the email of the contact you want to update: ");
        string email = Console.ReadLine()!;
        if (_contactService.CheckIfEmailExists(email))
        {
            var updatedContact = GetUpdatedContact();
            _contactService.UpdateContactInList(email, updatedContact);

            Console.WriteLine();
            Console.WriteLine($"Contact {updatedContact.FirstName} {updatedContact.FirstName} updated successfully.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"No contact with email <{email}> could be found.");
        }
        DisplayPressAnyKey();
    }

    private IContact GetUpdatedContact()
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

    private void ShowDeleteContactOption()
    {
        DisplayMenuTitle("DELETE CONTACT");

        Console.Write("Enter the email of the contact you want to delete: ");
        string email = Console.ReadLine()!;

        var ok = _contactService.RemoveContactFromList(email);
        if (ok)
        {
            _contactService.RemoveContactFromList(email);
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










    //public void StartMenu()
    //{
    //    bool repeat = true;

    //    do
    //    {
    //        DisplayMenu();
    //        Console.Write("\nYour choice: ");
    //        //int choice = int.Parse(Console.ReadLine()!);
    //        if (int.TryParse(Console.ReadLine(), out int choice))
    //        {
    //            Console.Clear();
    //            switch (choice)
    //            {
    //                case 0:
    //                    Console.WriteLine("Exiting program");
    //                    repeat = false;
    //                    break;
    //                case 1:
    //                    ShowAddContactOption();
    //                    break;
    //                case 2:
    //                    ShowContactInfoOption();
    //                    break;
    //                case 3:
    //                    ShowAllContactsOption();
    //                    break;
    //                case 4:
    //                    ShowUpdateContactOption();
    //                    break;
    //                case 5:
    //                    ShowDeleteContactOption();
    //                    break;
    //            }
    //        }
    //        else
    //            Console.WriteLine("Whoops that's not a number. Try again!");
    //    }
    //    while (repeat);
    //}

    //public void ShowUpdateContactOption()
    //{
    //    Console.Write("Enter the email of the contact you want to update: ");
    //    string email = Console.ReadLine()!;
    //    if (_contactService.CheckIfEmailExists(email))
    //    {
    //        var updatedContact = GetUpdatedContact();
    //        _contactService.UpdateContactInList(email, updatedContact);


    //        Console.WriteLine($"Contact {updatedContact.FirstName} {updatedContact.FirstName} updated successfully.");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"No contact with email <{email}> could be found. Press any key to continue.");
    //        Console.ReadKey();
    //    }
    //}
    //public IContact GetUpdatedContact()
    //{
    //    Console.Write("Enter new first name: ");
    //    string firstName = Console.ReadLine()!;
    //    Console.Write("Enter new last name: ");
    //    string lastName = Console.ReadLine()!;
    //    Console.Write("Enter new phone number: ");
    //    string phoneNumber = Console.ReadLine()!;
    //    Console.Write("Enter new email: ");
    //    string updatedEmail = Console.ReadLine()!;
    //    Console.Write("Enter new address: ");
    //    string address = Console.ReadLine()!;
    //    Console.WriteLine();

    //    var contact = new Contact
    //    {
    //        FirstName = firstName,
    //        LastName = lastName,
    //        PhoneNumber = phoneNumber,
    //        Email = updatedEmail,
    //        Address = address
    //    };
    //    return contact;
    //}

    //public void ShowDeleteContactOption()
    //{
    //    Console.Write("Enter the email of the contact you want to delete: ");
    //    string email = Console.ReadLine()!;

    //    var contact = _contactService.RemoveContactFromList(email);
    //    if (!contact)
    //    {
    //        Console.WriteLine("Contact not found. Press any key to continue.");
    //        Console.ReadKey();
    //    }
    //    else
    //    {
    //        _contactService.RemoveContactFromList(email);
    //        Console.WriteLine($"Contact with email {email} removed successfully.");
    //    }
    //}

    //public void ShowContactInfoOption()
    //{
    //    Console.Write("Enter the email of the contact: ");
    //    string email = Console.ReadLine()!;

    //    var foundContact = _contactService.GetContactFromList(email)!;

    //    if (foundContact is IContact contact)
    //    {
    //        Console.WriteLine(contact.FirstName);
    //        Console.WriteLine(contact.LastName);
    //        Console.WriteLine(contact.PhoneNumber);
    //        Console.WriteLine(contact.Email);
    //        Console.WriteLine(contact.Address);
    //        Console.WriteLine();
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Contact with email <{email}> not found. Press any key to continue.");
    //        Console.ReadKey();
    //    }
    //}

    //public void DisplayMenu()
    //{
    //    string[] menuOptions = ["Exit Program", "Add Contact", "Get Contact", "Get Contacts", "Update Contact", "Delete Contact"];

    //    for (int i = 0; i < menuOptions.Length; i++)
    //    {
    //        Console.WriteLine($"{i}. {menuOptions[i],-3}");
    //    }
    //}

    //public void ShowAddContactOption()
    //{
    //    Console.Write("Enter first name: ");
    //    string firstName = Console.ReadLine()!;
    //    Console.Write("Enter last name: ");
    //    string lastName = Console.ReadLine()!;
    //    Console.Write("Enter phone number: ");
    //    string phoneNumber = Console.ReadLine()!;
    //    Console.Write("Enter email: ");
    //    string email = Console.ReadLine()!;
    //    Console.Write("Enter address: ");
    //    string address = Console.ReadLine()!;
    //    Console.WriteLine();

    //    _contactService.AddContactToList(new Contact()
    //    {
    //        FirstName = firstName,
    //        LastName = lastName,
    //        PhoneNumber = phoneNumber,
    //        Email = email,
    //        Address = address
    //    });
    //}

    //public void ShowAllContactsOption()
    //{
    //    var contacts = _contactService.GetAllContactsFromList();

    //    if (contacts is List<IContact> list && list.Count > 0)
    //    {
    //        foreach (var contact in list)
    //        {
    //            Console.WriteLine(contact.FirstName);
    //            Console.WriteLine(contact.LastName);
    //            Console.WriteLine(contact.PhoneNumber);
    //            Console.WriteLine(contact.Email);
    //            Console.WriteLine(contact.Address);
    //            Console.WriteLine();
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("Your address book is empty. Press any key to continue.");
    //        Console.ReadKey();
    //    }
    //}
}
