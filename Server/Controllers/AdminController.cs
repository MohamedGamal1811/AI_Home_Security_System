//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Server.Date;
//using Server.Exceptions;
//using Server.Models.Entities;
//using System.Security.Claims;

//namespace Server.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // get all houses
//        //[HttpGet("houses")]
//        //public async Task<ActionResult<IEnumerable<House>>> GetHouses()
//        //{
//        //   // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        //    var houses =  await _context.Houses.Include(h => h.ApplicationUser).ToListAsync();
//        //    return houses;
//        //}

//        // get a house details by id
//        //[HttpGet("houses/{id}")]
//        //public async Task<ActionResult<House>> GetHouse(int id)
//        //{
//        //   // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        //    var house = await _context.Houses.Include(h=> h.ApplicationUser)
//        //        .FirstOrDefaultAsync(h => h.HouseId == id);

//        //    if (house == null) 
//        //        return NotFound("House not found");

//        //    return house;
//        //}

//        // delete a house
//        [HttpDelete("houses/{id}")]
//        public async Task<IActionResult> DeleteHouse(int id)
//        {
//           // var userId = ApplicationUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var house = await _context.Houses.FindAsync(id);

//            if (house == null ) 
//                return NotFound();

//            _context.Houses.Remove(house);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpGet("users")]
//        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
//        {
//            var users = await _userManager.Users
//                .Select(u => new
//                {
//                    u.Id,
//                    u.UserName,
//                    u.Email,
//                    u.PhoneNumber,
//                    Roles = (from userRole in _context.UserRoles
//                             join role in _context.Roles on userRole.RoleId equals role.Id
//                             where userRole.UserId == u.Id
//                             select role.Name).ToList()
//                })
//                .ToListAsync();

//            return Ok(users);
//        }

//        // GET: api/admin/users/{id}
//        [HttpGet("users/{id}")]
//        public async Task<ActionResult<object>> GetUserById(string id)
//        {
//            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
//            if (user == null) return NotFound();

//            var roles = await _userManager.GetRolesAsync(user);

//            return Ok(new
//            {
//                user.Id,
//                user.UserName,
//                user.Email,
//                user.PhoneNumber,
//                Roles = roles
//            });
//        }

//        // Delete: api/admin/users
//        [HttpDelete("users/{id}")]
//        public async Task<IActionResult> DeleteUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null) return NotFound();

//            var result = await _userManager.DeleteAsync(user);
//            if (!result.Succeeded) return BadRequest("Failed to delete user.");

//            return NoContent();
//        }
//    }
//}
