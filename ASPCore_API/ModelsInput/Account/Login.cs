using System.ComponentModel.DataAnnotations;

namespace ASPCore_API.ModelsInput.Account
{
    public class Login
    {
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType("Password")]
            public string Password { get; set;}
    }
}