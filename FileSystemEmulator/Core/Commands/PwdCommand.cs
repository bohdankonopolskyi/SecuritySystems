namespace Core.Commands;

public class PwdCommand : ICommand
{
    private readonly IFileSystem _fileSystem;

    public PwdCommand(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.GetPath();
    }
}