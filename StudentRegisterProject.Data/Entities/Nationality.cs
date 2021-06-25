using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Data.Entities
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }
    }
}
