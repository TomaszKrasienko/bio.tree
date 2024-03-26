using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.User;

public sealed record Role
{
    private readonly List<string> _availableRoles = ["User", "Admin"];

    public string Value { get; set; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyUserRoleException();
        }

        if (!_availableRoles.Contains(value))
        {
            throw new UnavailableRoleException(value);
        }

        Value = value;
    }

    public static implicit operator Role(string value)
        => new Role(value);

    public static implicit operator string(Role role)
        => role.Value;
}