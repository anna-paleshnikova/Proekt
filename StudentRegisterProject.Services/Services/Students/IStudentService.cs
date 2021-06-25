using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Students
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudents(string query = null);
        Task<StudentDTO> GetStudent(int id);
        Task CreateStudent(StudentDTO student);
        Task<StudentDTO> UpdateStudent(StudentDTO student, int id);
        Task DeleteStudent(int id);
    }
}
