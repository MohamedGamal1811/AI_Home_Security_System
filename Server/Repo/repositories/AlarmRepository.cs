using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Exceptions;
using Server.Models.DTOs.AlarmDTO;
using Server.Models.DTOs.NotificationDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly IGenericRepository<Alarm> _repository;
        private readonly IGenericRepository<House> _houseRepo;
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        
        public AlarmRepository(IGenericRepository<Alarm> repository, IGenericRepository<House> houseRepo, IMapper mapper, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _houseRepo = houseRepo;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        async Task IAlarmRepository.AddTriggerAlarmAsync(AlarmTriggerDTO alarmTriggerDTO)
        {
            var alarm = new Alarm
            {
                TriggeredAt = DateTime.UtcNow,
                DeviceId = alarmTriggerDTO.DeviceId,
                HouseId = alarmTriggerDTO.HouseId,
                Severity = alarmTriggerDTO.Severity,
                AutoResponseTriggered = true,
                TriggerType = alarmTriggerDTO.TriggerType,
            };

            await _repository.AddAsync(alarm);

            var user   = await _houseRepo.Query()
                .Where(h =>h.HouseId == alarmTriggerDTO.HouseId)
                .Select(h => h.Devices)
                .FirstOrDefaultAsync();

            if(user != null)
            {
                var notification = new CreateNotificationDTO
                {
                    Type = "AlarmTriggered",
                    Message = $"Alarm Triggered : {alarmTriggerDTO.TriggerType} in House {alarmTriggerDTO.HouseId},Severity : {alarmTriggerDTO.Severity}",
                    DeviceId = alarmTriggerDTO.DeviceId,
                    HouseId = alarmTriggerDTO.HouseId,
                    CreatedAt = DateTime.UtcNow,
                    //alarm == alarm
                };

               await _notificationRepository.AddAsync(notification);

            }
        }

        public async Task<IQueryable<GetAlarmTriggerDTO>> GetAlarmForHouseAsync(Guid houseId)
        {
            var alarm = await _repository.Query()
                 .Where(a => a.HouseId == houseId)
                 .OrderByDescending(a => a.TriggeredAt)
                 .ToListAsync();

            if (alarm == null)
                throw new NotFoundException($"Cant found alarm for this House Check Id:{houseId}");
            var alarmDTo = _mapper.Map<List<GetAlarmTriggerDTO>>(alarm);
            return alarmDTo.AsQueryable();
        }


        public async Task MarkAsFalsedAlarmAsync(Guid alarmId)
        {
            var alarm = await _repository.GetByIdAsync(alarmId);
            if (alarm != null)
            {
                alarm.IsFalseAlarm = true;
                await _repository.UpdateAsync(alarm);
            }
            ;
        }
    }
}
