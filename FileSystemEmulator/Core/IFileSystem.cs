using Core.Composite;

namespace Core;

public interface IFileSystem
{
    public string CurrentFile { get; }
    public string CurrentDirectory { get; }

    string GetUserGroup(string username, string password);

    void CreateDirectory(string directoryName);
    string GetPath();
    List<FileSystemComponent> GetChildren();
    void ChangeDirectory(string path);
    void Vi(string fileName);
    public void ModifyFile(string content);
    void Remove(string name);
}