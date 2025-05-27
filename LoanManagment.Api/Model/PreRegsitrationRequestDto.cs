using Application.Model;
using System.ComponentModel.DataAnnotations;

namespace LoanManagment.Api.Model
{
    public class PreRegsitrationRequestDto
    {
        [Required]
        public string NationalCode { get; set; }

        [RegularExpression(@"^09\d{9}$", ErrorMessage = "Phone number must start with '09' and be 11 characters")]
        [Required]
        public string Phonenumber { get; set; }

        [Required]
        public List<CreditModel> Items { get; set; }
    }
}
