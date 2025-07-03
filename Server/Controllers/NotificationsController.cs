using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.DTOs.NotificationDTO;
using Server.Repo.interfaces;
using System.Security.Claims;


//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepository _notificationRepo;

    public NotificationsController(INotificationRepository notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllNotifications()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var notificationDTOs = await _notificationRepo.GetAllAsync();
        return Ok(notificationDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotification(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var notificationDTO = await _notificationRepo.GetByIdAsync(id);
        if (notificationDTO == null)
            throw new NotFoundException($"Notification With this Id:{id},not Exist");

        return Ok(notificationDTO);
    }

    [HttpPost("add-notification")]
    public async Task<IActionResult> AddNotification(CreateNotificationDTO notificationDTO)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _notificationRepo.AddAsync(notificationDTO);
        if (!response.Success)
            return BadRequest(response);
        return response.Success ? Ok(notificationDTO) : BadRequest(response.Message);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _notificationRepo.DeleteAsync(id);
        if (!response.Success)
            throw new Exception(response.Message);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

}


