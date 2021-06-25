using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Data.Entities
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        [Required]
        public string City { get; set; }
        [MaxLength(30)]
        [Required]
        public string Address { get; set; }
    }
}
