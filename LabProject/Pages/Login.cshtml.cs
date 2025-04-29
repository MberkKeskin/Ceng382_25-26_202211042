using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System.IO;

using System.Text.Json;

using LabProject.Models;

using Microsoft.AspNetCore.Http;

using System;

using System.Linq;

using Microsoft.Extensions.Logging;



namespace LabProject.Pages

{

    public class LoginModel : PageModel

    {

        private readonly ILogger<LoginModel> _logger;



        public LoginModel(ILogger<LoginModel> logger)

        {

            _logger = logger;

        }



        [BindProperty]

        public string Username { get; set; } = string.Empty;



        [BindProperty]

        public string Password { get; set; } = string.Empty;



        public string ErrorMessage { get; set; } = string.Empty;



        public IActionResult OnGet(string? errorMessage)

        {

            // Eğer zaten giriş yaptıysa yönlendir

            if (HttpContext.Session.GetString("username") != null)

            {

                return RedirectToPage("/Index");

            }



            // TempData'dan hata mesajı al (tercihen)

            if (TempData["ErrorMessage"] != null)

            {

                ErrorMessage = TempData["ErrorMessage"]!.ToString();

            }

            else if (!string.IsNullOrEmpty(errorMessage))

            {

                ErrorMessage = errorMessage;

            }



            return Page();

        }



        public IActionResult OnPostAsync()

        {   var root = Directory.GetCurrentDirectory();

var filePath = Path.Combine(root, "wwwroot", "data", "users.json");



// TEMP LOG

_logger.LogInformation($"PROJECT ROOT: {root}");

_logger.LogInformation($"Looking for file: {filePath}");



if (!System.IO.File.Exists(filePath))

{

    ErrorMessage = "User data file not found.";

    _logger.LogError($"File not found at: {filePath}");

    return Page();

}



            var usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "users.json");

            _logger.LogInformation($"Attempting to read user data from: {usersFilePath}");



            if (!System.IO.File.Exists(usersFilePath))

            {

                ErrorMessage = "User data file not found.";

                _logger.LogError($"File not found: {usersFilePath}");

                return Page();

            }



            var jsonString = System.IO.File.ReadAllText(usersFilePath);

            var users = JsonSerializer.Deserialize<List<User>>(jsonString);



            if (users == null)

            {

                ErrorMessage = "Failed to read user data.";

                return Page();

            }



            var user = users.FirstOrDefault(u =>

                u.Username.Equals(Username, StringComparison.OrdinalIgnoreCase) &&

                u.Password == Password && u.IsActive);



            if (user != null)

            {

                var token = Guid.NewGuid().ToString();

                var sessionId = HttpContext.Session.Id;

                var cookieOptions = new CookieOptions

                {

                    Expires = DateTimeOffset.Now.AddMinutes(30),

                    HttpOnly = true,

                    Secure = true,

                    SameSite = SameSiteMode.Strict

                };



                HttpContext.Session.SetString("username", user.Username);

                HttpContext.Session.SetString("token", token);

                HttpContext.Session.SetString("session_id", sessionId);



                HttpContext.Response.Cookies.Append("username", user.Username, cookieOptions);

                HttpContext.Response.Cookies.Append("token", token, cookieOptions);

                HttpContext.Response.Cookies.Append("session_id", sessionId, cookieOptions);



                return RedirectToPage("/Index");

            }

            else

            {

                ErrorMessage = "Username or password is incorrect.";

                return Page();

            }

        }

    }

}