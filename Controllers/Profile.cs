using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Profile : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public string username = "";
        public string password = "";
        public string email = "";

        public Profile(ILogger<Profile>logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            //Get user id through session
            var userId = HttpContext.Session.GetInt32("UserId");
            username= HttpContext.Session.GetString("username");
            email = HttpContext.Session.GetString("useremail");
            password = HttpContext.Session.GetString("password");

            if(userId == null)
            {
                //if user id not exist then redirect back to login
                return RedirectToAction("Index", "Home");
            }
            var profileinfo = await GetProfile(userId.Value);

            return View("~/Views/Profile/Index.cshtml", profileinfo);
        }

        private async Task<ViewProfileModel>GetProfile(int userId)
        {
            var profile = new ViewProfileModel
            {
                Username = username,
                Email = email,
                Password = password
            };

            return profile;
        }


        [HttpPost]
        public async Task<IActionResult> Update(ViewProfileModel profile)
        {
            // Get user id
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // if user id not exist then redirect back to login
                return RedirectToAction("Index", "Home");
            }

            // call api to update user profile
            var isSuccess = await UpdateProfileInfo(userId.Value, profile);

            if (isSuccess)
            {
                // Update Successful
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View("Error");
            }
        }

        // Update User Information Method
        private async Task<bool> UpdateProfileInfo(int userId, ViewProfileModel profilemodel)
        {
            var apiUrl = $"https://localhost:7170/api/users/{userId}";
            var client = _httpClientFactory.CreateClient();

            var profileData = new ViewProfileModel
            {
                UserId = userId,
                Username = profilemodel.Username,
                Email = profilemodel.Email,
                Password = profilemodel.Password
            };
           
            var jsonContent = JsonSerializer.Serialize(profileData);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            // Read and log the actual content
            string actualContent = await content.ReadAsStringAsync();
            _logger.LogInformation($"API Json Content: {actualContent}");

            var response = await client.PutAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                // Set the new username in the session
                HttpContext.Session.SetString("username", profilemodel.Username);
            }

            return response.IsSuccessStatusCode;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
