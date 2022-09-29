using System.ComponentModel.DataAnnotations;

namespace DemoMvcCore.Models
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsDataEdited { get; set; }
    }
}
