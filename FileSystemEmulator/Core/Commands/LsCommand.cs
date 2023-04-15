namespace Core.Commands;

public class LsCommand : ICommand
{
    private readonly IFileSystem _fileSystem;

    public LsCommand(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.GetChildren();
    }
}