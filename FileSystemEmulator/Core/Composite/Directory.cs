namespace Core.Composite;

public class Directory : FileSystemComponent
{
    public Directory(string name) : base(name)
    {
    }

    public List<FileSystemComponent> Children { get; } = new();

    public override bool IsComposite()
    {
        return true;
    }

    public void Add(FileSystemComponent component)
    {
        Children.Add(component);
    }

    public void Remove(FileSystemComponent component)
    {
        Children.Remove(component);
    }

    public string GetFullPathByName(string name)
    {
        return GetFullPathByNameRecursive(this, name);
    }

    private string GetFullPathByNameRecursive(Directory directory, string name)
    {
        if (directory.Name == name) return $"{directory.Name}/";

        foreach (var child in directory.Children)
            if (child is Directory childDirectory)
            {
                var result = GetFullPathByNameRecursive(childDirectory, name);
                if (!string.IsNullOrEmpty(result)) return $"{directory.Name}/{result}";
            }

        return string.Empty;
    }
}