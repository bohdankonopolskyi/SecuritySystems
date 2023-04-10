namespace Core.Commands;

public sealed class CommandInvoker
{
    private Dictionary<string, ICommand> _commands;
    private static CommandInvoker _instance;
    
    private CommandInvoker()
    {
        _commands = new Dictionary<string, ICommand>();
    }

    public static CommandInvoker GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CommandInvoker();
        }

        return _instance;
    }
    public void AddCommand(string key, ICommand command)
    {
        _commands.Add(key, command);
    }

    public void ExecuteCommand(string commandKey)
    {
        var command = _commands[commandKey];
        if (command != null) 
            command.Execute();
    }
}