using AutoMapper;
using Server.Exceptions;
using Server.Models.DTOs;
using Server.Models.DTOs.NotificationDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IGenericRepository<Notification> _notificationRepo;
        private readonly IMapper _mapper;

        public NotificationRepository(IGenericRepository<Notification> notificationRepo, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddAsync(CreateNotificationDTO notificationDTO)
        {
            var notif = _mapper.Map<Notification>(notificationDTO);
            int result = await _notificationRepo.AddAsync(notif);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "Notification Added successfully" : "Failed to Added Notification"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _notificationRepo.DeleteAsync(id);
            return new ServiceResponse
            {
                Success = result > 0,
                Message = result > 0 ? "Notification Deleted successfully" : "Failed to Deleted Notification"
            };
        }

        public async Task<IEnumerable<GetNotificationDTO>> GetAllAsync()
        {
            var notifS = await _notificationRepo.GetAllAsync();
            if (notifS == null || !notifS.Any())
            {
                return Enumerable.Empty<GetNotificationDTO>();
            }
            var notifSDto = _mapper.Map<IEnumerable<GetNotificationDTO>>(notifS);
            return notifSDto;
        }

        public async Task<GetNotificationDTO> GetByIdAsync(Guid id)
        {
            var notif = await _notificationRepo.GetByIdAsync(id);
            if (notif == null)
            {
                throw new NotFoundException($"Notification with id:{id} not found");
            }
            var notifDTO = _mapper.Map<GetNotificationDTO>(notif);
            return notifDTO;
        }
    }
}