using System.ComponentModel.DataAnnotations;

namespace bio.tree.client.Models.Commands.User;

public class SignUpCommand
{
    [Required(ErrorMessage = "Please provide email")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Please provide first name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please provide last name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Please provide nickname")]
    public string Nickname { get; set; }
    
    [Required(ErrorMessage = "Please provide password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Please provide password confirmation")]
    [Compare(nameof(Password), ErrorMessage = "Passwords are not the same")]
    public string PasswordConfirmation { get; set; }
}