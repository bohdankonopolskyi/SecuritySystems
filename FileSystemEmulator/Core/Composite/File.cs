namespace Core.Composite;

public class File : FileSystemComponent
{
    public string Content;

    public File(string name) : base(name)
    {
    }

    public override bool IsComposite()
    {
        return false;
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + " " + Name);
    }
}