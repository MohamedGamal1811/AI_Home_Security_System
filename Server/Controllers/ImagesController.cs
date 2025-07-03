using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Date;
using Server.Models.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImagesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("upload-image-user")]
        [Authorize]
        public async Task<IActionResult> UploadUserImage(IFormFile image, [FromForm] string memberName, [FromForm] string relation, [FromServices] IHttpClientFactory httpClientFactory)
        {
            if (string.IsNullOrEmpty(memberName))
                return BadRequest("Member Name is Required");

            string name = User.Identity.Name;
            var applicationUser = await _userManager.FindByNameAsync(name);
            if (applicationUser == null)
                return Unauthorized("User not found");

            if (image == null || image.Length == 0)
                return BadRequest("Image Not Found");

            var repeated =  await _context.UserImages.FirstOrDefaultAsync(p=>p.Name == memberName && p.Relation == relation);

            if (repeated!= null )
                return BadRequest("Member Already Has Been Saved Before");
           

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileExtension = Path.GetExtension(image.FileName).ToLower();
            if (fileExtension != ".jpg")
                return BadRequest("Only .jpg files are allowed");

            string fileName = $"{memberName}{fileExtension}";
            string fullPath = Path.Combine(uploadsFolder, fileName);
            string relativePath = Path.Combine("UserImages", fileName).Replace("\\", "/");

            // Save the image locally
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Send image to FastAPI server
            var httpClient = httpClientFactory.CreateClient();
            var requestContent = new MultipartFormDataContent();

            using (var imageStream = System.IO.File.OpenRead(fullPath))
            {
                var streamContent = new StreamContent(imageStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");//changed from jpeg to jpg

                requestContent.Add(streamContent, "file", fileName);

                var fastApiResponse = await httpClient.PostAsync("http://10.184.240.79:8000/upload-face", requestContent);
                var fastApiResponseBody = await fastApiResponse.Content.ReadAsStringAsync();

                if (!fastApiResponse.IsSuccessStatusCode)
                    return StatusCode((int)fastApiResponse.StatusCode, $"FastAPI Error: {fastApiResponseBody}");
            }

            var userImage = new UserImage
            {
                OwnerUserId = applicationUser.Id,
                Name = memberName,
                FileName = fileName,
                ImagePath = relativePath,
                UploadedAt = DateTime.Now,
                //IsOwnerImage = isOwnerImage,
                Relation = relation
            };
            // save at DB
            _context.UserImages.Add(userImage);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Image Saved Successfully and Sent to AI Model",
                imageurl = $"{Request.Scheme}://{Request.Host}/{relativePath}",
                userImage
            });
        }


        [HttpPost("receive-from-ai")]
        [AllowAnonymous]
        public async Task<IActionResult> ReceiveImageFromAI(IFormFile file, [FromForm] string name, [FromForm] string classification, [FromForm] string timestamp)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Image Not Found");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(classification) || string.IsNullOrEmpty(timestamp))
                return BadRequest("Invalid data received");

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ReceivedImages");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";
            string fullPath = Path.Combine(uploadsFolder, fileName);
            string relativePath = Path.Combine("ReceivedImages", fileName).Replace("\\", "/");

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var receivedImage = new ReceivedImage
            {
                Name = name,
                Classification = classification,
                TimeStamp = DateTime.Parse(timestamp),
                ImagePath = relativePath
            };

            // Save History
            var history = new History
            {
                image = relativePath,
                name = name,
                status = classification,
                date = DateTime.Parse(timestamp)
            };

            await _context.Histories.AddAsync(history);
            await _context.SaveChangesAsync();


            await _context.ReceivedImages.AddAsync(receivedImage);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Image and data received successfully",
                            imageurl = $"{Request.Scheme}://{Request.Host}/{relativePath}",
                            receivedImage });
        }


        [HttpGet("Family-Members")]
        public async Task<IActionResult> RetrieveFamily()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            var result = _context.UserImages.Select(p => new
            {
                Name = p.Name
                ,
                Photo = baseUrl + p.ImagePath.Replace("\\", "/")
                ,
                relation = p.Relation 
                ,
                Id = p.Id
            });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("update-photo")]
        public async Task<IActionResult> updateFamilyMemberPhoto(IFormFile newImage, [FromForm] string memberName, [FromForm] string relation, [FromServices] IHttpClientFactory httpClientFactory)
        {
            if (string.IsNullOrEmpty(memberName) || string.IsNullOrEmpty(relation))
                return BadRequest("Member name and relation are required");

            if (newImage == null || newImage.Length == 0)
                return BadRequest("No image found");

            string userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return Unauthorized("User not found");

            var existingMember = await _context.UserImages.LastOrDefaultAsync(x => x.Name == memberName && x.Relation == relation && x.OwnerUserId == user.Id);

            if (existingMember == null)
                return NotFound("Family member not found");

            string extension = Path.GetExtension(newImage.FileName).ToLower();

            if (extension != ".jpg")
                return BadRequest("Only .jpg not allowed");


            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages");

            string fullPath = Path.Combine(uploadsFolder,existingMember.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await newImage.CopyToAsync(stream);
            }

            // Send to AI
            var httpClient = httpClientFactory.CreateClient();
            var formContent = new MultipartFormDataContent();

            using (var imgStream = System.IO.File.OpenRead(fullPath))
            {
                var streamContent = new StreamContent(imgStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");
                formContent.Add(streamContent, "file", existingMember.FileName);

                var response = await httpClient.PostAsync("http://192.168.1.30:8000/upload-face", formContent);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"AI Error: {error}");
                }
            }

            existingMember.UploadedAt = DateTime.Now;
            _context.UserImages.Update(existingMember);
            await _context.SaveChangesAsync();
            var relativePath = $"UserImages/{existingMember.FileName}";
            var fullImageUrl = $"{Request.Scheme}://{Request.Host}/{relativePath}";

            return Ok(new
            {
                message = "Photo updated successfully and sent to AI",
                photo = fullImageUrl,
                member = existingMember
            });

        }
    }


}
