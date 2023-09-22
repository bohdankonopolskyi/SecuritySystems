using Core.Access;
using Core.Composite;
using Directory = Core.Composite.Directory;
using File = Core.Composite.File;

namespace Core;

public sealed class FileSystem : IFileSystem
{
    private static FileSystem _instance;
    private readonly Directory _root = new("~");
    private readonly Security _security = new();
    private FileSystemComponent _currentComponent;
    private string _currentUserGroup;
    private readonly List<string> _users;

    public FileSystem()
    {
        _users = new List<string> { "root", "user123g" };
        _security.AddAccessPermission(_root, _users[0], "readwrite");
        _security.AddAccessPermission(_root, _users[1], "read");
        CurrentDirectory = _root;
    }

    public File CurrentFile { get; private set; }

    public Directory CurrentDirectory { get; private set; }

    public void ChangeUser(string username)
    {
        if (!_users.Contains(username))
            throw new Exception("Invalid username");
        _currentUserGroup = username;
    }

    //pwd
    public string GetPath()
    {
        return _root.GetFullPathByName(CurrentDirectory.Name);
    }

    //ls
    public List<FileSystemComponent> GetChildren()
    {
        return CurrentDirectory.Children;
    }

    //cd
    public void ChangeDirectory(string directoryName)
    {
        if (directoryName == "~/")
        {
            CurrentDirectory = _root;
            return;
        }

        var directory = GetComponentByName(directoryName) as Directory;

        if (directory == null)
            throw new Exception("Directory not found.");

        if (!_security.HasAccess(directory, _currentUserGroup, "read"))
            throw new Exception("Access denied.");

        CurrentDirectory = directory;
    }

    //mkdir
    public void CreateDirectory(string directoryName)
    {
        if (!_security.HasAccess(CurrentDirectory, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");

        if (GetComponentByName(directoryName) != null)
            throw new Exception("Directory already exists.");

        var directory = new Directory(directoryName);
        CurrentDirectory.Add(directory);

        _security.AddAccessPermission(directory, _currentUserGroup, "readwrite");
    }

    //vi
    public void Vi(string fileName)
    {
        if (!_security.HasAccess(CurrentDirectory, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");

        var file = GetComponentByName(fileName) as File;

        if (file == null)
        {
            file = new File(fileName);
            CurrentDirectory.Add(file);

            _security.AddAccessPermission(file, _currentUserGroup, "readwrite");
        }

        CurrentFile = file;
    }

    public void ModifyFile(string content)
    {
        if (CurrentFile == null)
            throw new Exception("File error");
        CurrentFile.Content = content;
    }

//rm
    public void Remove(string componentName)
    {
        var component = GetComponentByName(componentName);

        if (component == null)
            throw new Exception("Component not found.");

        if (!_security.HasAccess(component, _currentUserGroup, "readwrite"))
            throw new Exception("Access denied.");

        CurrentDirectory.Remove(component);
    }

    public static FileSystem GetInstance()
    {
        if (_instance == null)
            _instance = new FileSystem();

        return _instance;
    }

    private FileSystemComponent GetComponentByName(string componentName)
    {
        foreach (var component in CurrentDirectory.Children)
            if (component.Name.ToLower() == componentName.ToLower())
                return component;
        return null;
    }
}