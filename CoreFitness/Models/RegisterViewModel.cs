using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    
    public string Email { get; set; } = "";
}