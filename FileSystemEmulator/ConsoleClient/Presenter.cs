using Core;
using Core.Commands;

namespace ConsoleClient;

public class Presenter
{
    private static Dictionary<string, CommandType> CommandTypes = new Dictionary<string, CommandType>()
    {
        { "cd", CommandType.Cd },
        { "ls", CommandType.Ls },
        { "mkdir", CommandType.Mkdir },
        { "pwd", CommandType.Pwd },
        { "rm", CommandType.Rm },
        { "vi", CommandType.Vi }
    };

    private static IFileSystem _fileSystem = new FileSystem();
    private static CommandInvoker _invoker = new CommandInvoker();

    public static void Execute(string command, string input = "")
    {
        var commandType = CommandTypes[command.ToLower()];
        if (commandType == null)
            throw new Exception($"Invalid command: {command}");
        
        ICommand commandInstance = new FileSystemCommand(_fileSystem, commandType, input);
        _invoker.SetCommand(commandInstance);
        _invoker.Invoke();
    }
}