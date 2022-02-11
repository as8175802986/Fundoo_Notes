using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.User
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Userid { get; set; }
        
        public string Fname { get; set; }
        
        public string Lname { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string email { get; set; }
       
        public string Password { get; set; }
      
        public DateTime RegisteredDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
