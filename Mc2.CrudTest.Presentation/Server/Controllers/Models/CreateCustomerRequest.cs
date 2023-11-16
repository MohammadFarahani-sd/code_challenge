using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

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


        [Required] 
        [MaxLength(15)] 
        public string PhoneNumber { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
        
        public string? BankAccountNumber { get; set; }
    }
}
