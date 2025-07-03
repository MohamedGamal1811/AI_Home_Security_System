using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Server.Date;
using Server.Models.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController (ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; 
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("static")]
        public async Task<IActionResult> StaticProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return Unauthorized("User not found");
            var email = await _userManager.GetEmailAsync(user);
            var phone = await _userManager.GetPhoneNumberAsync(user);
            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            var p = _context.UserImages.OrderBy(p => p.UploadedAt).LastOrDefault(p => p.Relation.ToLower() == "father");
            //    .Select(p => new
            //{
            //    image = baseUrl+ p.ImagePath,
            //    name = p.Name,
            //    email = email,
            //    phone = phone
            //});

            return Ok(new
            {
                image = baseUrl + p.ImagePath,
                name = p.Name,
                email = email,
                phone = phone
            });
            

        }        
    } 
        
    


    
}


