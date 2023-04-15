namespace Core.Commands;

public class CommandInvoker
{
    private readonly List<ICommand> _commands;
    private ICommand _command;

    public CommandInvoker()
    {
        _commands = new List<ICommand>();
    }

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void Invoke()
    {
        _commands.Add(_command);
        _command.Execute();
    }
}