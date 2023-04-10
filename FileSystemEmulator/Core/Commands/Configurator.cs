using System.Runtime.CompilerServices;

namespace Core.Commands;

public static class Configurator
{
    private static IFileSystem _fileSystem = FileSystem.GetInstance();
    private static CommandInvoker _commandInvoker = CommandInvoker.GetInstance();

    public static void Configure()
    {
        // var cdCommand = new CdCommand(_)
        // _commandInvoker.AddCommand();
    }
}