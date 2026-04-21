using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Models;

public class SetPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(100)]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(100)]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = "";

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
    public bool AcceptTerms { get; set; }
}