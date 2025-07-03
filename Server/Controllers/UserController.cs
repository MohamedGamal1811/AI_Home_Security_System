//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Server.Date;
//using Server.Models.Entities;
//using System.Security.Claims;

//namespace Server.Controllers
//{
//    [Authorize(Roles = "ApplicationUser,Admin")]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public UserController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [Authorize(Roles = "ApplicationUser,Admin")]
//        [HttpGet("me")]  // Unique route to get the current user's details
//        public async Task<ActionResult<ApplicationUser>> GetUserDetails()
//        {
//            var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            if (userId == null)
//            {
//                return Unauthorized("ApplicationUser not found");
//            }

//            var user = await _context.Users
//                .Where(u => u.Id == userId)
//                .FirstOrDefaultAsync();

//            if (user == null)
//            {
//                return NotFound("ApplicationUser not found");
//            }

//            return Ok(user);
//        }

//        // ✅ Get all users (Admin only)
//        [Authorize(Roles = "Admin")]
//        [HttpGet("all")]  // Unique route to get all users
//        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
//        {
//            return await _context.Users.ToListAsync();
//        }
//    }
//}