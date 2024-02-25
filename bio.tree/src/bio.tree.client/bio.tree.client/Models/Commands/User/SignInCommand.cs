using System.ComponentModel.DataAnnotations;

namespace bio.tree.client.Models.Commands.User;

public class SignInCommand
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required] 
    public string Password { get; set; }
}