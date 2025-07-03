using AutoMapper;
using Server.Models.DTOs.AlarmDTO;
using Server.Models.DTOs.DeviceDTo;
using Server.Models.DTOs.HistoryDTO;
using Server.Models.DTOs.HouseDTO;
using Server.Models.DTOs.NotificationDTO;
using Server.Models.Entities;

namespace Server.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //house mapping
            CreateMap<House, GetHouseDTO>();
            CreateMap<House, CreateHouseDTO>().ReverseMap();
            CreateMap<House, UpdateHouseDTO>().ReverseMap();

            //device mapping
            CreateMap<Device, GetDeviceDTO>();
            CreateMap<Device, CreateDeviceDTO>().ReverseMap();
            CreateMap<Device, UpdateDeviceDTO>().ReverseMap();

            //history mapping
            CreateMap<History, GetHistoryDto>();
            CreateMap<History, CreateHistoryDto>().ReverseMap();
            CreateMap<History, UpdateHistoryDto>().ReverseMap();

            //Notification mapping
            CreateMap<Notification, GetNotificationDTO>();
            CreateMap<Notification, CreateNotificationDTO>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDTO>().ReverseMap();
            
            //Alarm mapping
            CreateMap<Alarm, GetAlarmTriggerDTO>();
            CreateMap<Alarm, AlarmTriggerDTO>().ReverseMap();
 
            
        }
    }
 
}
