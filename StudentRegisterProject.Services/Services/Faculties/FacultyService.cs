using Microsoft.EntityFrameworkCore;
using StudentRegisterProject.Data.Context;
using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Faculties
{
    public class FacultyService : IFacultyService
    {
        private readonly StudentRegisterDBContext context;

        public FacultyService(StudentRegisterDBContext context)
        {
            this.context = context;
        }

        public async Task CreateFaculty(FacultyDTO facultyDto)
        {
            var uuid = Guid.NewGuid().GetHashCode();
            var faculty = new Faculty
            {
                Id = facultyDto.Id,
                Name = facultyDto.Name,
                City = facultyDto.City,
                Address = facultyDto.Address,
                CreatedBy = uuid,
                UpdatedBy = uuid,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            await context.Faculties.AddAsync(faculty);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFaculty(int id)
        {
            context.Faculties.Remove(context.Faculties.Find(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllFaculties(string query = null)
        {
            var faculties = await context.Faculties.ToListAsync();
            if (!String.IsNullOrEmpty(query))
            {
                faculties = faculties.Where(f => f.Name.ToLower().Contains(query.ToLower())).ToList();
            }
            List<FacultyDTO> facultyDtos = new List<FacultyDTO>();
            foreach (var faculty in faculties)
            {
                facultyDtos.Add(new FacultyDTO
                {
                    Address = faculty.Address,
                    City = faculty.City,
                    Name = faculty.Name,
                    Id = faculty.Id
                });
            }
            return facultyDtos;
        }

        public async Task<FacultyDTO> GetFaculty(int id)
        {
            var faculty = await context.Faculties.FirstOrDefaultAsync(f => f.Id == id);
            var facultyDto = new FacultyDTO
            {
                Address = faculty.Address,
                City = faculty.City,
                Name = faculty.Name,
                Id = faculty.Id
            };
            return facultyDto;
        }

        public async Task<FacultyDTO> UpdateFaculty(FacultyDTO facultyDto, int id)
        {

            var updatedFaculty = await context.Faculties.FirstOrDefaultAsync(f => f.Id == id);
            updatedFaculty.Name = facultyDto.Name;
            updatedFaculty.City = facultyDto.City;
            updatedFaculty.Address = facultyDto.Address;
            updatedFaculty.UpdatedOn = DateTime.Now;

            await context.SaveChangesAsync();
            var returnFaculty = new FacultyDTO
            {
                Address = updatedFaculty.Address,
                City = updatedFaculty.City,
                Name = updatedFaculty.Name,
                Id = updatedFaculty.Id
            };
            return returnFaculty;
        }
    }
}
