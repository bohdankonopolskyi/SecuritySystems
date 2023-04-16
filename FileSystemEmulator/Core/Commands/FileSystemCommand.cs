namespace Core.Commands;

public class FileSystemCommand : ICommand
{
    private readonly CommandType _commandType;
    private readonly IFileSystem _fileSystem;
    private readonly string _input;

    public FileSystemCommand(IFileSystem fileSystem, CommandType type, string input = "")
    {
        _fileSystem = fileSystem;
        _input = input;
        _commandType = type;
    }

    public void Execute()
    {
        switch (_commandType)
        {
            case CommandType.Cd:
                Cd();
                break;
            case CommandType.Ls:
                Ls();
                break;
            case CommandType.Mkdir:
                Mkdir();
                break;
            case CommandType.Pwd:
                Pwd();
                break;
            case CommandType.Rm:
                Rm();
                break;
            case CommandType.Vi:
                Vi();
                break;
            case CommandType.Su:
                Su();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Cd()
    {
        try
        {
            _fileSystem.ChangeDirectory(_input);
            Console.WriteLine(_fileSystem.GetPath());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Ls()
    {
        foreach (var component in _fileSystem.GetChildren()) Console.WriteLine(component.Name);
    }

    private void Mkdir()
    {
        try
        {
            _fileSystem.CreateDirectory(_input);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Pwd()
    {
        Console.WriteLine(_fileSystem.GetPath());
    }

    private void Vi()
    {
        try
        {
            _fileSystem.Vi(_input);
            Console.WriteLine($"{_fileSystem.CurrentFile.Name}: ");
            Console.Write(_fileSystem.CurrentFile.Content);
            var content = Console.ReadLine();
            if (content == "")
                return;
            _fileSystem.ModifyFile(content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Rm()
    {
        try
        {
            _fileSystem.Remove(_input);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Su()
    {
        try
        {
            _fileSystem.ChangeUser(_input);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}