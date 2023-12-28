using Newtonsoft.Json;
using Shared.Interfaces;
using System.Diagnostics;

namespace Shared.Services;

public class ContactService : IContactService
{
    private List<IContact> _contactList = [];
    private readonly IFileService _fileService;
    private readonly string _filePath = @"c:\Projects\contacts.json";

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetAllContactsFromList().ToList();
    }

    private void SaveContactsToFile()
    {
        var jsonContent = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
        });

        _fileService.SaveContentToFile(_filePath, jsonContent);
    }

    public bool AddContactToList(IContact contact)
    {
        try
        {
            if (!_contactList.Any(x => x.Email == contact.Email))
            {
                _contactList.Add(contact);

                SaveContactsToFile();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    // Hämta json-informationen och få in den i listan
    public IEnumerable<IContact> GetAllContactsFromList()
    {
        try
        {
            var jsonContent = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(jsonContent))
            {
                // Deserializera som en lista av typen IContact
                _contactList = JsonConvert.DeserializeObject<List<IContact>>(jsonContent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                })!;

            }
            return _contactList;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public IContact GetContactFromList(string email)
    {
        try
        {
            GetAllContactsFromList();

            var contact = _contactList.FirstOrDefault(x => x.Email == email);

            return contact ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public bool RemoveContactFromList(string email)
    {
        try
        {
            if (!string.IsNullOrEmpty(email))
            {
                var contactToRemove = _contactList.FirstOrDefault(x => x.Email == email);

                if (contactToRemove is not null)
                {
                    _contactList.Remove(contactToRemove);

                    SaveContactsToFile();

                    return true;
                }
            }

            return false; // contact with the given email not found
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool UpdateContactInList(string email, IContact contact)
    {
        try
        {
            var existingContact = GetContactFromList(email);

            if (existingContact != null)
            {
                _contactList.Remove(existingContact);

                AddContactToList(contact);

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool CheckIfEmailExists(string email)
    {
        var existingContact = GetContactFromList(email);
        if (existingContact != null)
            return true;
        else
            return false;
    }
}
