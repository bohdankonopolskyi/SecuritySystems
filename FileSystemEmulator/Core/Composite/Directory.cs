namespace Core.Composite;

public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> _children = new List<FileSystemComponent>();

    public List<FileSystemComponent> Children
    {
        get
        {
            return _children;
        }
    }
    public Directory(string name) : base(name) { }

    public override bool IsComposite()
    {
        return true;
    }
    
    public void Add(FileSystemComponent component)
    {
        _children.Add(component);
    }

    public void Remove(FileSystemComponent component)
    {
        _children.Remove(component);
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + "+ " + Name);
        foreach (FileSystemComponent component in _children)
        {
            component.Display(depth + 2);
        }
    }
}