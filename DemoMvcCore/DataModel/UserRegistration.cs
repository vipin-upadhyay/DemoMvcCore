using System.ComponentModel.DataAnnotations;

namespace DemoMvcCore.DataModel
{
    public class UserRegistration
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDataedited { get; set; }
    }
}
