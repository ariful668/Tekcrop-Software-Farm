using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechProject.Models
{
    public partial class ContactPerson
    {
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Message { get; set; }
    }
}
