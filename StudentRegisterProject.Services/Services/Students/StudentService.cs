using Microsoft.EntityFrameworkCore;
using StudentRegisterProject.Data.Context;
using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly StudentRegisterDBContext context;

        public StudentService(StudentRegisterDBContext context)
        {
            this.context = context;
        }
        public async Task CreateStudent(StudentDTO student)
        {
            var uuid = Guid.NewGuid().GetHashCode();
            var nationality = context.Nationalities.FirstOrDefault(s => s.Id == student.NationalityId);

            var faculty = context.Faculties.FirstOrDefault(s => s.Id == student.FacultyId);
            var studentSave = new Student
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                FacultyNumber = student.FacultyNumber,
                Nationality = nationality,
                Faculty = faculty,
                NationalityId = nationality.Id,
                FacultyId = faculty.Id,
                CreatedBy = uuid,
                UpdatedBy = uuid,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            await context.Students.AddAsync(studentSave);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            context.Students.Remove(context.Students.Find(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents(string query = null)
        {
            var students = await context.Students.Include(s => s.Nationality)
                                                 .Include(s => s.Faculty).ToListAsync();
            if (!String.IsNullOrEmpty(query))
            {
                students = students.Where(s => (s.FirstName.ToLower() + " " + s.LastName.ToLower()).Contains(query.ToLower())).ToList();
            }
            List<StudentDTO> studentDtos = new List<StudentDTO>();
            foreach (var item in students)
            {
                studentDtos.Add(new StudentDTO
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    FacultyNumber = item.FacultyNumber,
                    Nationality = new NationalityDTO
                    {
                        Id = item.Nationality.Id,
                        Title = item.Nationality.Title
                    },
                    Faculty = new FacultyDTO
                    {
                        Id = item.Faculty.Id,
                        Name = item.Faculty.Name,
                        City = item.Faculty.City,
                        Address = item.Faculty.Address
                    }
                });
            }
            return studentDtos;
        }

        public async Task<StudentDTO> GetStudent(int id)
        {
            var student = await context.Students.Include(s => s.Nationality)
                                                .Include(s => s.Faculty)
                                                .FirstOrDefaultAsync(s => s.Id == id);
            var studentDto = new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                FacultyNumber = student.FacultyNumber,
                Nationality = new NationalityDTO
                {
                    Id = student.NationalityId,
                    Title = student.Nationality.Title
                },
                Faculty = new FacultyDTO
                {
                    Id = student.FacultyId,
                    Name = student.Faculty.Name,
                    City = student.Faculty.City,
                    Address = student.Faculty.Address
                }
            };
            return studentDto;
        }

        public async Task<StudentDTO> UpdateStudent(StudentDTO student, int id)
        {
            var updatedStudent = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            updatedStudent.FirstName = student.FirstName;
            updatedStudent.LastName = student.LastName;
            updatedStudent.FacultyNumber = student.FacultyNumber;
            updatedStudent.Faculty = new Faculty { 
                Id = student.Faculty.Id,
                Name = student.Faculty.Name,
                City = student.Faculty.City,
                Address = student.Faculty.Address,
                UpdatedOn = DateTime.Now,
                CreatedBy = updatedStudent.Faculty.UpdatedBy,
                UpdatedBy = updatedStudent.Faculty.UpdatedBy,
                CreatedOn = updatedStudent.Faculty.CreatedOn
            };
            updatedStudent.FacultyId = student.FacultyId;
            updatedStudent.Nationality = new Nationality {
                Id = student.Nationality.Id,
                Title = student.Nationality.Title,
                UpdatedOn = DateTime.Now,
                CreatedBy = updatedStudent.Nationality.UpdatedBy,
                UpdatedBy = updatedStudent.Nationality.UpdatedBy,
                CreatedOn = updatedStudent.Nationality.CreatedOn
            };
            updatedStudent.NationalityId = student.NationalityId;
            updatedStudent.UpdatedOn = DateTime.Now;

            await context.SaveChangesAsync();

            var studentDto = new StudentDTO
            {
                Id = updatedStudent.Id,
                Faculty = student.Faculty,
                Nationality = student.Nationality,
                FacultyId = updatedStudent.FacultyId,
                FacultyNumber = updatedStudent.FacultyNumber,
                FirstName = updatedStudent.FirstName,
                LastName = updatedStudent.LastName,
                NationalityId = updatedStudent.NationalityId

            };
            return studentDto;
        }
    }
}
