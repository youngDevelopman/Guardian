using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guardian.AuthorizationService.Models
{
    public class UserPool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserPoolId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
