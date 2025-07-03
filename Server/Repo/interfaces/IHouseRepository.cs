using Server.Models.DTOs;
using Server.Models.DTOs.HouseDTO;
using Server.Models.Entities;

namespace Server.Repo.interfaces
{
    public interface IHouseRepository
    {
        public Task<IEnumerable<GetHouseDTO>> GetAllAsync();
        public Task<GetHouseDTO> GetByIdAsync(Guid id);
        public Task<ServiceResponse> AddAsync(CreateHouseDTO houseDTO);
        public Task<ServiceResponse> UpdateAsync(UpdateHouseDTO houseDTO);
        public Task<ServiceResponse> DeleteAsync(Guid id);
       // public IQueryable<House> Query();
    }
}
