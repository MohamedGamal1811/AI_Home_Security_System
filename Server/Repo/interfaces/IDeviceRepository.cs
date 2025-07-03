using Server.Models.DTOs;
using Server.Models.DTOs.DeviceDTo;

namespace Server.Repo.interfaces
{
    public interface IDeviceRepository
    {
        public Task<IEnumerable<GetDeviceDTO>> GetAllAsync();
        public Task<GetDeviceDTO> GetByIdAsync(Guid id);
        public Task<ServiceResponse> AddAsync(CreateDeviceDTO houseDTO);
        public Task<ServiceResponse> UpdateAsync(UpdateDeviceDTO houseDTO);
        public Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
