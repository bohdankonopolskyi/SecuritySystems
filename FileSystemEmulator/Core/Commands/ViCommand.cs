namespace Core.Commands;

public class ViCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _fileName;

    public ViCommand(IFileSystem fileSystem, string fileName)
    {
        _fileSystem = fileSystem;
        _fileName = fileName;
    }

    public void Execute()
    {
        _fileSystem.Vi(_fileName);
    }
}