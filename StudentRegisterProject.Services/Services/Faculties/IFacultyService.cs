using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Faculties
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDTO>> GetAllFaculties(string query = null);
        Task<FacultyDTO> GetFaculty(int id);
        Task CreateFaculty(FacultyDTO facultyDto);
        Task<FacultyDTO> UpdateFaculty(FacultyDTO facultyDto, int id);
        Task DeleteFaculty(int id);
    }
}
