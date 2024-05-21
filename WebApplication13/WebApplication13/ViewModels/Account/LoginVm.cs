
using System.ComponentModel.DataAnnotations;

namespace WebApplication13.ViewModels.Account
{
    public class LoginVm
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}
