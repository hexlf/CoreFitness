using System.ComponentModel.DataAnnotations;


namespace CoreFitness.Models;

public class UpdateProfileViewModel
{
    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(100)]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(100)]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = "";

    [MaxLength(30)]
    public string? PhoneNumber { get; set; }

    public IFormFile? ProfileImage { get; set; }

    public string? ExistingProfileImagePath { get; set; }
}