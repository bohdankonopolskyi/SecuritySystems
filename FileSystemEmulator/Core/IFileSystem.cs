using Core.Composite;
using Directory = Core.Composite.Directory;
using File = Core.Composite.File;

namespace Core;

public interface IFileSystem
{
    public File CurrentFile { get; }
    public Directory CurrentDirectory { get; }

    void ChangeUser(string username);

    void CreateDirectory(string directoryName);
    string GetPath();
    List<FileSystemComponent> GetChildren();
    void ChangeDirectory(string path);
    void Vi(string fileName);
    public void ModifyFile(string content);
    void Remove(string name);
}