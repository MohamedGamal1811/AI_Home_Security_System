using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.DTOs.HistoryDTO;
using Server.Repo.interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoriesRepository _historyRepo;

        public HistoryController(IHistoriesRepository historyRepo)
        {
            _historyRepo = historyRepo;
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAllHistory()
        {


            var histories = await _historyRepo.GetAllAsync();

            var result = histories.Select(h => new
            {
                date = h.date,
                name = h.name,
                status = h.status,
                image = $"{Request.Scheme}://{Request.Host}/{h.image}"
            }).ToList();

            return Ok(result);

            #region
            //[HttpGet("{id}")]
            //public async Task<ActionResult> GetHistory(Guid id)
            //{
            //    var history = await _historyRepo.GetByIdAsync(id);

            //    if (history == null)
            //        throw new NotFoundException($"History With this Id:{id},not Exist");
            //    return Ok(history);
            //}
            //[HttpPost("add-history")]
            //public async Task<ActionResult> AddHistory([FromForm] CreateHistoryDto file)
            //{
            //    if (file.FileURl == null || file.FileURl.Length == 0)
            //    {
            //        return BadRequest("File is required.");
            //    }

            //    try
            //    {
            //        using var memoryStream = new MemoryStream();
            //        await file.FileURl.CopyToAsync(memoryStream);
            //        var fileBytes = memoryStream.ToArray();

            //        var base64String = Convert.ToBase64String(fileBytes);

            //        var history = new CreateHistoryDto
            //        {
            //            ImageUrl = base64String,
            //            date = DateTime.UtcNow,
            //        };

            //        var result = await _historyRepo.AddAsync(history);

            //        if (!result.Success)
            //        {
            //            return BadRequest(new { message = result.Message });
            //        }


            //        return Ok(new { message = "History added successfully.", history = result.Message });
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            //    }
            //}

            //    [HttpDelete("delete/{id}")]
            //    public async Task<IActionResult> DeleteHistory(Guid id)
            //    {
            //        // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //        try
            //        {
            //            await _historyRepo.DeleteAsync(id);
            //            return Ok("History deleted successfully.");
            //        }
            //        catch (NotFoundException ex)
            //        {
            //            return NotFound(new { message = ex.Message });
            //        }
            //        catch (Exception ex)
            //        {
            //            return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            //        }

            //    }
            //}public async Task<GetHistoryDto> GetByIdAsync(Guid id)
            //{
            //    var history = await _historyRepo.GetByIdAsync(id);
            //    if (history == null)
            //    {
            //        throw new NotFoundException("House not found");
            //    }
            //    // Map the House entity to a GetHouseDTO
            //    var historyDTO = _mapper.Map<GetHistoryDto>(history);

            //    return historyDTO;
            //}
            #endregion
        }
    }

}