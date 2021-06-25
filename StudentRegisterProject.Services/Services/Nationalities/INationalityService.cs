using StudentRegisterProject.Data.Entities;
using StudentRegisterProject.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegisterProject.Services.Services.Nationalities
{
    public interface INationalityService
    {
        Task<IEnumerable<NationalityDTO>> GetAllNationalities(string query = null);
        Task<NationalityDTO> GetNationality(int id);
        Task CreateNationality(NationalityDTO nationality);
        Task<NationalityDTO> UpdateNationality(NationalityDTO nationality, int id);
        Task DeleteNationality(int id);
    }
}
