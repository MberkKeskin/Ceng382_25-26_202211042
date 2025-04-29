using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace LabProject.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnPost()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("session_id");
        }

        public void OnGet()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("session_id");
        }
    }
}
