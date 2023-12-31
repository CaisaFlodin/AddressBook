using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System.Diagnostics;

namespace Shared.Services;

public class ContactService : IContactService
{
    private List<Contact> _contactList = [];
    private readonly IFileService _fileService;
    private readonly string _filePath = @"c:\Projects\AddressBook\contacts.json";

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetAllContactsFromList().ToList();
    }

    public event EventHandler? ContactsUpdated;

    private bool SaveContactsToFile()
    {
        var jsonContent = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
        });

        var ok = _fileService.SaveContentToFile(_filePath, jsonContent);
        if (ok)
            return true;
        else
            return false;
    }

    public bool AddContactToList(Contact contact)
    {
        try
        {
            if (!_contactList.Any(x => x.Email == contact.Email) && !string.IsNullOrEmpty(contact.Email))
            {
                _contactList.Add(contact);

                SaveContactsToFile();

                ContactsUpdated?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - AddContactToList:: " + ex.Message); }
        return false;
    }

    public IEnumerable<Contact> GetAllContactsFromList()
    {
        try
        {
            var jsonContent = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(jsonContent))
            {
                // Deserializera som en lista av typen Contact
                _contactList = JsonConvert.DeserializeObject<List<Contact>>(jsonContent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                })!;

                return _contactList;
            }
            else
            {
                return Enumerable.Empty<Contact>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - GetAllContactsFromList:: " + ex.Message);
            return null!;
        }
    }

    public Contact GetContactFromList(string email)
    {
        try
        {
            GetAllContactsFromList();

            var contact = _contactList.FirstOrDefault(x => x.Email == email);

            return contact ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - GetContactFromList:: " + ex.Message); }
        return null!;
    }

    public bool RemoveContactFromList(Contact contact)
    {
        try
        {
            var existingContact = _contactList.FirstOrDefault(x => x.Email == contact.Email);

            if (existingContact is not null)
            {
                _contactList.Remove(existingContact);

                SaveContactsToFile();
               
                ContactsUpdated?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - RemoveContactFromList:: " + ex.Message); }
        return false;
    }

    public bool UpdateContactInList(Contact updatedContact)
    {
        try
        {
            var existingContact = _contactList.FirstOrDefault(x => x.Email == updatedContact.Email);

            if (existingContact != null)
            {
                existingContact.FirstName = updatedContact.FirstName;
                existingContact.LastName = updatedContact.LastName;
                existingContact.Phone = updatedContact.Phone;
                existingContact.Email = updatedContact.Email;
                existingContact.Address = updatedContact.Address;

                SaveContactsToFile();

                ContactsUpdated?.Invoke(this, EventArgs.Empty);

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - UpdateContactInList:: " + ex.Message);
            return false;
        }
    }
}
