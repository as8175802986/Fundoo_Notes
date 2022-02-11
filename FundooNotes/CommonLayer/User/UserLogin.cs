using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{


    public class UserLogin
    {
        [Required(ErrorMessage = "user name is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }

}
