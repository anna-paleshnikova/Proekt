using Microsoft.EntityFrameworkCore;
using StudentRegisterProject.Data.Context;
using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Nationalities
{
    public class NationalityService : INationalityService
    {
        private readonly StudentRegisterDBContext context;

        public NationalityService(StudentRegisterDBContext context)
        {
            this.context = context;
        }

        public async Task CreateNationality(NationalityDTO nationalityDto)
        {
            var uuid = Guid.NewGuid().GetHashCode();
            var nationality = new Nationality
            {
                Id = nationalityDto.Id,
                Title = nationalityDto.Title,
                CreatedBy = uuid,
                UpdatedBy = uuid,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            await context.Nationalities.AddAsync(nationality);
            await context.SaveChangesAsync();
        }

        public async Task DeleteNationality(int id)
        {
            context.Nationalities.Remove(context.Nationalities.Find(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NationalityDTO>> GetAllNationalities(string query = null)
        {
            var nationalities = await context.Nationalities.ToListAsync();
            if (!String.IsNullOrEmpty(query))
            {
                nationalities = nationalities.Where(n => n.Title.ToLower().Contains(query.ToLower())).ToList();
            }
            List<NationalityDTO> nationalityDtos = new List<NationalityDTO>();
            foreach (var item in nationalities)
            {
                nationalityDtos.Add(new NationalityDTO
                {
                    Id = item.Id,
                    Title = item.Title
                });
            }
            return nationalityDtos;
        }

        public async Task<NationalityDTO> GetNationality(int id)
        {
            var nationality = await context.Nationalities.FirstOrDefaultAsync(n => n.Id == id);
            var nationalityDto = new NationalityDTO
            {
                Id = nationality.Id,
                Title = nationality.Title
            };
            return nationalityDto;
        }

        public async Task<NationalityDTO> UpdateNationality(NationalityDTO nationality, int id)
        {
            var updatedNationality = await context.Nationalities.FirstOrDefaultAsync(n => n.Id == id);
            updatedNationality.Title = nationality.Title;
            updatedNationality.UpdatedOn = DateTime.Now;

            await context.SaveChangesAsync();
            var nationalityDto = new NationalityDTO
            {
                Id = updatedNationality.Id,
                Title = updatedNationality.Title
            };
            return nationalityDto;
        }
    }
}
