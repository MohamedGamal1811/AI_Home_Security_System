
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.DTOs.DeviceDTo;
using Server.Repo.interfaces;
using System.Security.Claims;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IDeviceRepository _deviceRepo;

    public DevicesController(IDeviceRepository deviceRepo)
    {
        _deviceRepo = deviceRepo;
    }


    // Gets all houses for the authenticated user.
    [HttpGet("All")]
    public async Task<ActionResult> GetAllDevices()
    {
        var deviceId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var devices = await _deviceRepo.GetAllAsync();
        return Ok(devices);

    }

    // get a house details by id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetDevice(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var device = await _deviceRepo.GetByIdAsync(id);

        if (device == null)
            throw new NotFoundException($"Device With this Id:{id},not Exist");
        return Ok(device);
    }

    // Adds a new house.  
    [HttpPost("add-device")]
    public async Task<ActionResult> AddDevice(CreateDeviceDTO deviceDTO)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _deviceRepo.AddAsync(deviceDTO);
        return result.Success ? Ok(deviceDTO) : BadRequest(result.Message);
    }

    //update house details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDevice(UpdateDeviceDTO deviceDTO)
    {
        // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _deviceRepo.UpdateAsync(deviceDTO);

        return result.Success ? Ok(deviceDTO) : BadRequest(result.Message);
    }


    // Deletes a house by id.
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDevice(Guid id)
    {
        // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        try
        {
            await _deviceRepo.DeleteAsync(id);
            return Ok("device deleted successfully.");
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
        }

    }
}


