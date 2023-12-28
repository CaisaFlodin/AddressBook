namespace Shared.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Adds a new contact to the contact list if a contact with the same email does not already exist.
    /// </summary>
    /// <param name="contact">An object implementing the IContact interface to be added to the contact list.</param>
    /// <returns>
    ///   <c>true</c> if the contact is successfully added; otherwise, <c>false</c>.
    /// </returns>
    bool AddContactToList(IContact contact);

    /// <summary>
    /// Retrieves all contacts from the contact list by deserializing the content from a file.
    /// </summary>
    /// <returns>
    ///   An IEnumerable<IContact> containing all contacts in the list, or <c>null</c> if an error occurs.
    /// </returns>
    IEnumerable<IContact> GetAllContactsFromList();

    /// <summary>
    /// Retrieves a contact from the contact list based on the provided email address.
    /// </summary>
    /// <param name="email">The email address of the contact to retrieve.</param>
    /// <returns>
    ///   An IContact object representing the contact with the specified email, or <c>null</c> if not found or an error occurs.
    /// </returns>
    IContact GetContactFromList(string email);

    /// <summary>
    /// Updates a contact in the contact list with the provided email by removing the existing contact and adding a new one.
    /// </summary>
    /// <param name="email">The email of the contact to be updated.</param>
    /// <param name="contact">An object implementing the IContact interface with the updated information.</param>
    /// <returns>
    ///   <c>true</c> if the contact is successfully updated; otherwise, <c>false</c>.
    /// </returns>
    bool UpdateContactInList(string email, IContact contact);

    /// <summary>
    /// Removes a contact from the contact list based on the provided email address.
    /// </summary>
    /// <param name="email">The email address of the contact to be removed.</param>
    /// <returns>
    ///   <c>true</c> if the contact is successfully removed; otherwise, <c>false</c> if the email is null or empty,
    ///   or if no contact with the specified email is found. 
    /// </returns>
    bool RemoveContactFromList(string email);

    /// <summary>
    /// Checks if a contact with the provided email address exists in the contact list.
    /// </summary>
    /// <param name="email">The email address to check for existence in the contact list.</param>
    /// <returns>
    ///   <c>true</c> if a contact with the specified email exists; otherwise, <c>false</c>.
    /// </returns>
    bool CheckIfEmailExists(string email);
}
