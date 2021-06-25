using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Services.DTO
{
    public class FacultyDTO
    {
            [Key]
            public int Id { get; set; }

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
