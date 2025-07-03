using Server.Models.DTOs.AlarmDTO;

namespace Server.Repo.interfaces
{
    public interface IAlarmRepository
    {
        public Task AddTriggerAlarmAsync(AlarmTriggerDTO alarmTriggerDTO);
        public Task <IQueryable<GetAlarmTriggerDTO>>  GetAlarmForHouseAsync(Guid houseId);
        public Task MarkAsFalsedAlarmAsync(Guid alarmId);
    }
}
