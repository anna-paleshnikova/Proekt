using StudentRegisterProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Services.DTO
{
    public class StudentDTO
    {
        [Key]
        public int Id { get; set; }
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
        public virtual NationalityDTO Nationality { get; set; }
        [Required]
        public int FacultyId { get; set; }
        public virtual FacultyDTO Faculty { get; set; }
    }
}
