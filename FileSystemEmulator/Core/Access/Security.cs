using Core.Composite;

namespace Core.Access;

public class Security
{
    // Dictionary of access permissions for each component
    private Dictionary<FileSystemComponent, Dictionary<string, string>> _accessPermissions = new();

    public void AddAccessPermission(FileSystemComponent component, string userGroup, string accessLevel)
    {
        if (!_accessPermissions.ContainsKey(component))
        {
            _accessPermissions[component] = new Dictionary<string, string>();
        }

        _accessPermissions[component][userGroup] = accessLevel;
    }

    public string GetAccessLevel(FileSystemComponent component, string userGroup)
    {
        if (!_accessPermissions.ContainsKey(component))
        {
            return "";
        }

        if (!_accessPermissions[component].ContainsKey(userGroup))
        {
            return "";
        }

        return _accessPermissions[component][userGroup];
    }

    public bool HasAccess(FileSystemComponent component, string userGroup, string accessLevel)
    {
        if (!_accessPermissions.ContainsKey(component))
        {
            return false;
        }

        if (!_accessPermissions[component].ContainsKey(userGroup))
        {
            return false;
        }

        return _accessPermissions[component][userGroup] == accessLevel;
    }
}