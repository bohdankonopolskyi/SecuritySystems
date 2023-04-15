namespace Core.Commands;

public class CdCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _path;

    public CdCommand(IFileSystem fileSystem, string path)
    {
        _fileSystem = fileSystem;
        _path = path;
    }

    public void Execute()
    {
        _fileSystem.ChangeDirectory(_path);
    }
}