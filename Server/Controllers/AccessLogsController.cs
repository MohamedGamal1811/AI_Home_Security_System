//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Server.Date;
//using Server.Models.Entities;
//using System.Security.Claims;
//using Microsoft.EntityFrameworkCore;
//#region Alerts Controller
//#endregion

//[Authorize]
//[Route("api/[controller]")]
//[ApiController]
//public class AccessLogsController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    public AccessLogsController(ApplicationDbContext context) { _context = context; }

//    /// <summary>
//    /// Retrieves access logs for the authenticated user.
//    /// </summary>
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<AccessLog>>> GetAccessLogs()
//    {
//        var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        return NoContent();
//            //await _context.AccessLogs.Where(log => log.UserId == userId).ToListAsync();
//    }
//}

