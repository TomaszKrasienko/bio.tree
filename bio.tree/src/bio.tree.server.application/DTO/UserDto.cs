namespace bio.tree.shared.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public IEnumerable<UserLinkDto> UserLinks { get; set; }
}