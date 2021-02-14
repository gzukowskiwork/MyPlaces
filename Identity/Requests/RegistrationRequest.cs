using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Requests
{
    public class RegistrationRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password must be at least eight digits long")]
        public string Password { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm password not match")]
        public string ConfirmPassword { get; set; }
    }
}
