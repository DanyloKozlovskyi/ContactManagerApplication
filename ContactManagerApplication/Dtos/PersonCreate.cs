using System.ComponentModel.DataAnnotations;

namespace ContactManagerApplication.Dtos
{
    public class PersonCreate
    {
        [Required(ErrorMessage = $"{nameof(Name)} can't be blank")]
        public string? Name { get; set; }
        [Required(ErrorMessage = $"{nameof(DateOfBirth)} can't be blank")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = $"{nameof(Married)} can't be blank")]
        public bool Married { get; set; }
        [Required(ErrorMessage = $"{nameof(PhoneNumber)} can't be blank")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = $"{nameof(Salary)} can't be blank")]
        public decimal Salary { get; set; }
    }
}
