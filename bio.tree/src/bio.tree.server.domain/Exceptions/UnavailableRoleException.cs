namespace bio.tree.server.domain.Exceptions;

public class UnavailableRoleException(string value)
    : Exception($"Role: {value} is unavailable");