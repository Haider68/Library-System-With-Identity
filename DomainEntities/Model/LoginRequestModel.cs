﻿using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Model
{
    public class LoginRequestModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
