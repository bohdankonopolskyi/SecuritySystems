using ConsoleClient;

internal class Program
{
    private static void Main(string[] args)
    {
        
        Console.WriteLine("Enter username");
        Console.WriteLine("Enter username");
        Console.WriteLine("Enter username");
        var username = Console.ReadLine();
        Presenter.Execute("su", username);
        Presenter.Execute("pwd");

        while (true)
        {
            var command = Console.ReadLine();
            if (command == "q") break;

            try
            {
                var splitted = command.Split();

                if (splitted.Length > 1)
                    Presenter.Execute(splitted[0], splitted[1]);
                else if (splitted.Length == 1) Presenter.Execute(splitted[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}