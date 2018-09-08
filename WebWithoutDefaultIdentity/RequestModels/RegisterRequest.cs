using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebWithoutDefaultIdentity.RequestModels
{
    public class UserRequest
    {
        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), MaxLength(50), Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? StudentId { get; set; }
        public string Role { get; set; }
    }
}