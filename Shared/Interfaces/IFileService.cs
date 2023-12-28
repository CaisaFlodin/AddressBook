namespace Shared.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Saves the provided content to a file at the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to save the content to with extension (eg. c:\projects\myfile.json).</param>
    /// <param name="content">The content to be saved to the file as a string.</param>
    /// <returns>
    ///   <c>true</c> if the content is saved to the file; otherwise, <c>false</c>.
    /// </returns>
    bool SaveContentToFile(string filePath, string content);

    /// <summary>
    /// Retrieves the content of a file as a string from the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to retrieve content from with extension (eg. c:\projects\myfile.json).</param>
    /// <returns>
    ///   The content of the file as a string, or <c>null</c> if the file does not exist or an error occurs.
    /// </returns>
    string GetContentFromFile(string filePath);
}
