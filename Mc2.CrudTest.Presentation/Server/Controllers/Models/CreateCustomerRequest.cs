using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Presentation.Server.Controllers.Models
{
    public class CreateCustomerRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(128)]
        public string Firstname { get; set; } = null!;

        [Required]
        [MinLength(3)]
        [MaxLength(128)]
        public string Lastname { get; set; } = null!;


        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }
        
        public string? BankAccountNumber { get; set; }
    }
}