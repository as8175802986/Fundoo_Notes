using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        [RegularExpression(@"^[A-Z]{1}[a-zA-Z]{3,}$",
        ErrorMessage = "Please enter a valid Fname")]
        public string Fname { get; set; }

        [RegularExpression(@"^[A-Z]{1}[a-zA-Z]{3,}$",
        ErrorMessage = "Please enter a valid Lname")]
        public string Lname { get; set; }

        [RegularExpression(@"^[6-9]{1}[0-9]{9}$",
        ErrorMessage = "Please enter a valid Phone")]
        public string Phone { get; set; }

        [RegularExpression(@"^[A-Za-z]{3,}$",
        ErrorMessage = "Please enter a valid Adress")]
        public string Adress { get; set; }
        [RegularExpression(@"^[a-zA-z0-9]+(.[a-z0-9]+)?@[a-z]+[.][a-z]{3}$",
        ErrorMessage = "Please enter correct email address")]
        public string email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Please enter Strong Password")]
        public string Password { get; set; }

    }
}


    

