using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentRegisterProject.Services.DTO
{
    public class NationalityDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }
    }
}
