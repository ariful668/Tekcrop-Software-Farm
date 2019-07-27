using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechProject.Models
{
    public partial class JobPost
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Desp { get; set; }
        [Required]
        [StringLength(100)]
        public string Position { get; set; }
        [Required]
        public string Skill { get; set; }
        [Required]
        [StringLength(50)]
        public string Vacancy { get; set; }
        [Required]
        [StringLength(50)]
        public string Nature { get; set; }
        [Required]
        [StringLength(100)]
        public string Experience { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        [StringLength(50)]
        public string Salary { get; set; }

        public DateTime? AddedDate { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        public DateTime LastDate { get; set; }
    }
}
