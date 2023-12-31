using Shared.Models;

namespace Shared.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Adds a contact to the contact list if email doesn´t already exist.
    /// </summary>
    /// <param name="contact">The contact to be added to the list.</param>
    /// <returns>True if the contact is successfully added; otherwise, false.</returns>
    bool AddContactToList(Contact contact);

    /// <summary>
    /// Retrieves all contacts from a file and returns the list.
    /// </summary>
    /// <returns>List of contacts or null.</returns>
    IEnumerable<Contact> GetAllContactsFromList();

    /// <summary>
    /// Retrieves a contact from the contact list based on the provided email.
    /// </summary>
    /// <param name="email">The email of the contact to retrieve.</param>
    /// <returns>The contact with the specified email or null.</returns>
    Contact GetContactFromList(string email);

    /// <summary>
    /// Updates an existing contact with the provided contact information.
    /// </summary>
    /// <param name="updatedContact">The contact with updated information.</param>
    /// <returns>True if the contact is successfully updated; otherwise, false.</returns>
    bool UpdateContactInList(Contact contact);

    /// <summary>
    /// Removes a contact from the contact list.
    /// </summary>
    /// <param name="contact">The contact to be removed.</param>
    /// <returns>True if the contact is successfully removed; otherwise, false.</returns>
    bool RemoveContactFromList(Contact contact);
}
