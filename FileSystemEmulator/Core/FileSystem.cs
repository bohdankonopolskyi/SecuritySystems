using Core.Access;
using Core.Composite;
using Directory = Core.Composite.Directory;
using File = Core.Composite.File;

namespace Core;

public sealed class FileSystem : IFileSystem
{
    private static FileSystem _instance;
    private readonly Directory _root = new("~/");
    private readonly Security _security = new();
    private FileSystemComponent _currentComponent;
    private Directory _currentDirectory;
    private File _currentFile;
    private string _currentUserGroup;

    public FileSystem()
    {
        _security.AddAccessPermission(_root, "admin", "readwrite");
        _security.AddAccessPermission(_root, "user", "read");
        _currentDirectory = _root;
    }

    public string CurrentFile => _currentFile.Name;
    public string CurrentDirectory => _currentDirectory.Name;

    public string GetUserGroup(string username, string password)
    {
        if (username == "admin" && password == "adminpass") return "admin";

        if (username == "user" && password == "userpass") return "user";

        return "";
    }

    //pwd
    public string GetPath()
    {
        return _root.GetFullPathByName(_currentDirectory.Name);
    }

    //ls
    public List<FileSystemComponent> GetChildren()
    {
        return _currentDirectory.Children;
    }

    //cd
    public void ChangeDirectory(string directoryName)
    {
        var directory = GetComponentByName(directoryName) as Directory;

        if (directory == null)
            throw new Exception("Directory not found.");

        if (!_security.HasAccess(directory, _currentUserGroup, "read"))
            throw new Exception("Access denied.");

        _currentDirectory = directory;
    }

    //mkdir
    public void CreateDirectory(string directoryName)
    {
        if (!_security.HasAccess(_currentDirectory, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");

        if (GetComponentByName(directoryName) != null)
            throw new Exception("Directory already exists.");

        var directory = new Directory(directoryName);
        _currentDirectory.Add(directory);

        _security.AddAccessPermission(directory, _currentUserGroup, "readwrite");
    }

    //vi
    public void Vi(string fileName)
    {
        if (!_security.HasAccess(_currentComponent, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");

        var file = GetComponentByName(fileName) as File;

        if (file == null)
        {
            file = new File(fileName);
            _currentDirectory.Add(file);

            _security.AddAccessPermission(file, _currentUserGroup, "readwrite");
        }

        _currentFile = file;
    }

    public void ModifyFile(string content)
    {
        if (_currentFile == null)
            throw new Exception("File error");
        _currentFile.Content = content;
    }

//rm
    public void Remove(string componentName)
    {
        var component = GetComponentByName(componentName);

        if (component == null)
            throw new Exception("Component not found.");

        if (!_security.HasAccess(component, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");
        
        _currentDirectory.Remove(component);
    }

    public static FileSystem GetInstance()
    {
        if (_instance == null)
            _instance = new FileSystem();

        return _instance;
    }

    private FileSystemComponent GetComponentByName(string componentName)
    {
        foreach (var component in _currentDirectory.Children)
            if (component.Name.ToLower() == componentName.ToLower())
                return component;
        return null;
    }
}