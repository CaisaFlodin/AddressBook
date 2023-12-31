using Moq;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;

namespace AddressBook.Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToListShould_AddOneContactToContactList_ThenReturnTrue()
    {
        // Arrange
        Contact contact = new Contact { FirstName = "Test", LastName = "Testsson", Phone = "0733477354", Email = "test@domain.com", Address = "Testvägen 6" };

        var mockFileService = new Mock<IFileService>();
        ContactService contactService = new ContactService(mockFileService.Object);

        // Act
        bool result = contactService.AddContactToList(contact);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAllContactsFromListShould_GetAllContactsInContactList_ThenReturnListOfContacts()
    {
        // Arrange
        var contacts = new List<Contact> { new Contact { FirstName = "Test", LastName = "Testsson", Phone = "0733477354", Email = "test@domain.com", Address = "Testvägen 6" } };

        string json = JsonConvert.SerializeObject(contacts, Formatting.None, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.GetContentFromFile(It.IsAny<string>())).Returns(json);

        IContactService contactService = new ContactService(mockFileService.Object);

        // Act
        var result = contactService.GetAllContactsFromList();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
        Contact returned_contact = result.FirstOrDefault()!;
        Assert.Equal("test@domain.com", returned_contact.Email);
    }
}
