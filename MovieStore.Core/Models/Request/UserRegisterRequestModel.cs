﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public      class UserRegisterRequestModel
    {
        //DataAnnotations are useful for Validation in ASP.NET Core
        [Required]
        [EmailAddress]
        [StringLength(50)]       
        public string Email { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(20,ErrorMessage ="Make sure your password length is between 8 and 20", MinimumLength =8)]
        //password should be strong ,one upper&lower letter, a number and special character
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }
    }
}
