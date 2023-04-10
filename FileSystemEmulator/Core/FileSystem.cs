using Core.Access;
using Core.Composite;
using Directory = Core.Composite.Directory;
using File = Core.Composite.File;

namespace Core;

public sealed class FileSystem: IFileSystem
{
    private Security _security = new Security();
    private Directory _root = new Directory("");
    private static FileSystem _instance;
    private FileSystemComponent _currentComponent;
    public Directory _currentDirectory;
    
    public string CurrentComponentName => _currentComponent.Name;
    public string CurrentDirectoryName => _currentDirectory.Name;

    private string _currentUserGroup;
    private FileSystem()
    {
        // Set up default access permissions for the root directory
        _security.AddAccessPermission(_root, "admin", "readwrite");
        _security.AddAccessPermission(_root, "user", "read");
    }

    public static FileSystem GetInstance()
    {
        if (_instance == null)
            _instance = new FileSystem();

        return _instance;
    }
    // Method to prompt the user for their login credentials
    // public void Login()
    // {
    //     Console.WriteLine("Enter your username:");
    //     string username = Console.ReadLine();
    //     Console.WriteLine("Enter your password:");
    //     string password = Console.ReadLine();
    //
    //     string userGroup = GetUserGroup(username, password);
    //
    //     if (userGroup == "")
    //     {
    //         Console.WriteLine("Invalid login credentials. Exiting program...");
    //         Environment.Exit(0);
    //     }
    //
    //     Console.WriteLine("Welcome, " + userGroup + "!");
    // }

// Method to determine the user's group based on their login credentials
public string GetUserGroup(string username, string password)
    {
        // Normally, this information would be stored in a database or other external source
        if (username == "admin" && password == "adminpass")
        {
            return "admin";
        }

        if (username == "user" && password == "userpass")
        {
            return "user";
        }

        return "";
    }

// Method to display the current directory
    public void Pwd()
    {
        Console.WriteLine("Current directory: " + _currentComponent.Name);
    }
    

// Method to display the contents of the current directory
    public void Ls()
    {
        _currentComponent.Display(0);
    }

// Method to change the current directory
    public void Cd(string directoryName)
    {
        var directory = GetComponentByName(directoryName) as Directory;

        if (directory == null)
            throw new Exception("Directory not found.");

        if (!_security.HasAccess(directory, GetUserGroup(), "read"))
            throw new Exception("Access denied.");
        //
        // if (!(component is Directory))
        //     throw new Exception("Not a directory.");

        _currentDirectory = directory;
    }

// Method to create a new directory
    public void Mkdir(string directoryName)
    {
        if (!_security.HasAccess(_currentDirectory, GetUserGroup(), "readwrite"))
            throw new Exception("Access denied.");
     
        if (GetComponentByName(directoryName) != null)
            throw new Exception("Directory already exists.");

        Directory directory = new Directory(directoryName);
        _currentDirectory.Add(directory);

        _security.AddAccessPermission(directory, GetUserGroup(), "readwrite");
    }

// Method to create or edit a file
    public void Vi(string fileName)
    {
        if (!_security.HasAccess(_currentComponent, GetUserGroup(), "readwrite"))
        {
            Console.WriteLine("Access denied.");
            return;
        }

        File file = (File)GetComponentByName(fileName);

        if (file == null)
        {
            file = new File(fileName);
            _currentDirectory.Add(file);

            _security.AddAccessPermission(file, GetUserGroup(), "readwrite");

            Console.WriteLine("File created.");
        }

        Console.WriteLine("Enter file contents: ");
        file.Content = Console.ReadLine();

        Console.WriteLine("File saved.");
    }

// Method to delete a directory or file
    public void Rm(string componentName)
    {
        FileSystemComponent component = GetComponentByName(componentName);

        if (component == null)
        {
            Console.WriteLine("Component not found.");
            return;
        }

        if (!_security.HasAccess(component, GetUserGroup(), "readwrite"))
        {
            Console.WriteLine("Access denied.");
            return;
        }

        _currentDirectory.Remove(component);

        Console.WriteLine("Component deleted.");
    }
    
    // Helper method to get a component by name from the current directory
    private FileSystemComponent GetComponentByName(string componentName)
    {
        foreach (FileSystemComponent component in _currentDirectory.Children)
        {
            if (component.Name.ToLower() == componentName.ToLower())
            {
                return component;
            }
        }
        return null;
    }
    
    // Helper method to get the current user's group
    private string GetUserGroup()
    {
        // For the purposes of this example, we will assume that the user is already logged in
        // and their group has been determined based on their login credentials

        // In a real-world scenario, the user's group would need to be determined dynamically
        // based on their user ID or some other identifier

        return _currentUserGroup;
    }

}