namespace Shared.Interfaces
{
    /// <summary>
    /// Interface for representing contact information. 
    /// </summary>
    /// <remarks>
    /// This interface defines properties for accessing and modifying contact details.
    /// </remarks>
    public interface IContact
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string Address { get; set; }
    }
}
