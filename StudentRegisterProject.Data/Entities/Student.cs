using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        [MaxLength(15)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(15)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public long FacultyNumber { get; set; }
        [Required]
        public int NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        [Required]
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
