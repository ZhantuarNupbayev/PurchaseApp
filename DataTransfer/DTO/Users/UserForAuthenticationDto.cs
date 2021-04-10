﻿using System.ComponentModel.DataAnnotations;

namespace DataTransfer.DTO.Users
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
