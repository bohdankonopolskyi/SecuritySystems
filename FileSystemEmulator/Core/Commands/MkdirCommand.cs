namespace Core.Commands;

public class MkdirCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _directoryName;

    public MkdirCommand(IFileSystem fileSystem, string directoryName)
    {
        _fileSystem = fileSystem;
        _directoryName = directoryName;
    }

    public void Execute()
    {
        _fileSystem.Mkdir(_directoryName);
    }
}