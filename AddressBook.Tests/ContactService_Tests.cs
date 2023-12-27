using Moq;
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
        IContact contact = new Contact { FirstName = "Test", LastName = "Testsson", PhoneNumber = "0733477354", Email = "test@domain.com", Address = "Testvägen 6" };

        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactService(mockFileService.Object);


        // Act
        bool result = contactService.AddContactToList(contact);


        // Assert
        Assert.True(result);
    }
}
