using Core;
class Program
{
    static void Main(string[] args)
    {
        // Display the number of command line arguments.
        Console.WriteLine(args.Length);
    }
}

// using File = Core.Composite.File;
// using Directory = Core.Composite.Directory
// // Create a file
// File file = new File("file.txt");
//
// // Create a directory
// Directory directory = new Directory("directory");
//
// // Add the file to the directory
// directory.Add(file);
//
// // Create a security object
// Core.Security security = new Core.Security();
//
// // Set access permissions for the file and directory
// security.AddAccessPermission(file, "user1", true);
// security.AddAccessPermission(file, "user2", false);
// security.AddAccessPermission(directory, "user1", true);
// security.AddAccessPermission(directory, "user2", true);
//
// // Display the directory and its contents if the user has access
// string currentUser = "user1";
// if (directory.HasAccess(currentUser))
// {
//     directory.Display(0);
// }
//
// // Attempt to display the directory and its contents as a user without access
// currentUser = "user2";
// if (directory.HasAccess(currentUser))
// {
//     directory.Display(0);
// }