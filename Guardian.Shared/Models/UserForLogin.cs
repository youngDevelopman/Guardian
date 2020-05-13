using System;
using System.ComponentModel.DataAnnotations;

namespace Guardian.Shared.Models
{
    public class UserForLogin
    {
        [Required]
        public Guid UserPoolId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
