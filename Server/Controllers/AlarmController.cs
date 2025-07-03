using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.DTOs.AlarmDTO;
using Server.Repo.interfaces;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AlarmController : ControllerBase
{
    private readonly IAlarmRepository _alarmRepository;

    public AlarmController(IAlarmRepository alarmRepository)
    {
        _alarmRepository = alarmRepository;
    }

    [HttpPost("Trigger")]
    public async Task<IActionResult> TriggerAlarm(AlarmTriggerDTO alarmTriggerDTO)
    {
        await _alarmRepository.AddTriggerAlarmAsync(alarmTriggerDTO);
        return Ok("Alarm Trigger and Notification Sent.*");
    }

    [HttpGet("house/{id}")]
    public async Task<IActionResult> GetAlarmsForHouse(Guid id)
    {
        var alarm = await _alarmRepository.GetAlarmForHouseAsync(id);
        if (alarm == null)
            throw new NotFoundException("No Alarms for this house");
        return Ok(alarm);
    }

    [HttpPut("{id}/false")]
    public async Task<IActionResult> MarkedAsFalseAlarm(Guid id)
    {
        await _alarmRepository.MarkAsFalsedAlarmAsync(id);
        return Ok();
    }

}


