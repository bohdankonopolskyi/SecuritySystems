namespace Core.Commands;

public class RmCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _name;

    public RmCommand(IFileSystem fileSystem, string name)
    {
        _fileSystem = fileSystem;
        _name = name;
    }

    public void Execute()
    {
        _fileSystem.Rm(_name);
    }
}