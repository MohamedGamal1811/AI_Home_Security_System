using Server.Models.DTOs;
using Server.Models.DTOs.NotificationDTO;

namespace Server.Repo.interfaces
{
    public interface INotificationRepository
    {
        public Task<IEnumerable<GetNotificationDTO>> GetAllAsync();
        public Task<GetNotificationDTO> GetByIdAsync(Guid id);
        public Task<ServiceResponse> AddAsync(CreateNotificationDTO houseDTO);
        public Task<ServiceResponse> DeleteAsync(Guid id);
    }


}
