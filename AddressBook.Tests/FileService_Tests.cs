using Shared.Interfaces;
using Shared.Services;

namespace AddressBook.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveToFileShould_ReturnTrue_IfFilePathExists()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"c:\Projects\test.text";
        string content = "Test content";

        // Act
        bool result = fileService.SaveContentToFile(filePath, content);

        // Assert
        Assert.True(result);
    }
}
