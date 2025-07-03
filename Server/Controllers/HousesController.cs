using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.DTOs.HouseDTO;
using Server.Models.Entities;
using Server.Repo.interfaces;
using Server.Repo.repositories;
using System.Security.Claims;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly IHouseRepository _houseRepo;

    public HousesController(IHouseRepository houseRepo)
    {
        _houseRepo = houseRepo;
    }


    // Gets all houses for the authenticated user.

    [HttpGet("All")]
    public async Task<ActionResult> GetAllHouses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var houses = await _houseRepo.GetAllAsync();
        return Ok(houses);

    }

    // get a house details by id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetHouse(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var house = await _houseRepo.GetByIdAsync(id);

        if (house == null)
            throw new NotFoundException($"house With this Id:{id},not Exist");
        return Ok(house);
    }

    // Adds a new house.  
    [HttpPost("add-house")]
    public async Task<ActionResult> AddHouse(CreateHouseDTO house)
    {
        house.UserId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _houseRepo.AddAsync(house);

        return result.Success ? Ok(house): BadRequest(result.Message);
    }

    //update house details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHouse(UpdateHouseDTO updateHouseDTO)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _houseRepo.UpdateAsync(updateHouseDTO);
         
       return result.Success ? Ok(updateHouseDTO) : BadRequest(result.Message);
    }


    // Deletes a house by id.
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteHouse(Guid id)
    {
        // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        try
        {
            await _houseRepo.DeleteAsync(id);
            return Ok("House deleted successfully.");
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


