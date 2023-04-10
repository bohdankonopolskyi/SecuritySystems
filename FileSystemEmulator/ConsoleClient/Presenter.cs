using Core;
using Core.Commands;

namespace ConsoleClient;

public class Presenter
{
    private IFileSystem _fileSystem;
    private CommandInvoker _commandInvoker;

    public Presenter()
    {
        _fileSystem = FileSystem.GetInstance();
        _commandInvoker = CommandInvoker.GetInstance();
    }
    
    // Method to prompt the user for their login credentials
    public void Login()
    {
        Console.WriteLine("Enter your username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        string userGroup = _fileSystem.GetUserGroup(username, password);

        if (userGroup == "")
        {
            Console.WriteLine("Invalid login credentials. Exiting program...");
            Environment.Exit(0);
        }

        Console.WriteLine("Welcome, " + userGroup + "!");
    }
    
}