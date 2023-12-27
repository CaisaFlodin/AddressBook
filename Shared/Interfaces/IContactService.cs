namespace Shared.Interfaces;

public interface IContactService
{
    bool AddContactToList(IContact contact);

    IEnumerable<IContact> GetAllContactsFromList();

    IContact GetContactFromList(string email);

    bool UpdateContactInList(string email, IContact updatedContact);

    bool RemoveContactFromList(string email);

    bool CheckIfEmailExists(string email);
}
