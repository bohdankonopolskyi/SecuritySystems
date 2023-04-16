namespace Core.Composite;

public abstract class FileSystemComponent
{
    protected Dictionary<string, bool> _accessPermissions;

    public FileSystemComponent(string name)
    {
        Name = name;
        _accessPermissions = new Dictionary<string, bool>();
    }

    public string Name { get; set; }

    public virtual void AddAccessPermission(string user, bool hasAccess)
    {
        _accessPermissions[user] = hasAccess;
    }

    public virtual bool HasAccess(string user)
    {
        return _accessPermissions.ContainsKey(user) && _accessPermissions[user];
    }

    public abstract bool IsComposite();
}