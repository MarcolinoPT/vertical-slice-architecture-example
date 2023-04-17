using System.ComponentModel.DataAnnotations;

namespace Web.Features.Users
{
    public record UserRequest
    {
        [Required]
        public string? FirstName { get; init; }
        [Required]
        public string? LastName { get; init; }
        [Required]
        public string? StreetName { get; init; }
        [Required]
        public string? HouseNumber { get; init; }
        public string? ApartmentNumber { get; init; }
        [Required]
        public string? PostalCode { get; init; }
        [Required]
        public string? Town { get; init; }
        [Required]
        public string? PhoneNumber { get; init; }
        [Required]
        [RegularExpression("\\d{4}-\\d{2}-\\d{2}")]
        public string? DateOfBirth { get; init; }
    }
}
