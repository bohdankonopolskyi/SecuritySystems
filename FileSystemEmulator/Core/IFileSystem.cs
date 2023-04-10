namespace Core;

public interface IFileSystem
{
    string GetUserGroup(string username, string password);
    string CurrentDirectoryName { get; }

     void Mkdir(string directoryName);
     void Pwd();
     void Ls();
     void Cd(string path);
     void Vi(string fileName);
     void Rm(string name);
}