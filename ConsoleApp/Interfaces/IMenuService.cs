using Shared.Interfaces;

namespace ConsoleApp.Interfaces;

public interface IMenuService
{
    void StartMenu();
    void ShowAddContactOption();
    void ShowContactsOption();
    void ShowContactInfoOption();
    void ShowDeleteContactOption();
    void ShowUpdateContactOption();
    IContact GetUpdatedContact();
}
