using System.ComponentModel.DataAnnotations;

namespace Webshop.DTOs;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;


    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; } = string.Empty;


    [StringLength(200, ErrorMessage = "Street address can't be longer than 200 characters.")]
    public string StreetAddress { get; set; } = string.Empty;
}
