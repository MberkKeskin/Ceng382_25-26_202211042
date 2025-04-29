using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabProject.Models;
using LabProject.Helpers;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
        // Veriler
        public static List<ClassInformationModel> ClassList { get; set; } = new();
        public static List<ClassInformationModel> DisplayClassList { get; set; } = new();

        // Sayfada gösterilecek veriler
        public List<ClassInformationTable> PagedList { get; set; } = new();

        // Arama ve sayfa bilgileri
        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }
        public const int PageSize = 10;

        // Sayfa yüklendiğinde çalışır
        public IActionResult OnGet()
        {
            // Kullanıcı giriş kontrolü
            string? sessionUsername = HttpContext.Session.GetString("username");
            string? sessionToken = HttpContext.Session.GetString("token");
            string? sessionSessionId = HttpContext.Session.GetString("session_id");

            string? cookieUsername = HttpContext.Request.Cookies["username"];
            string? cookieToken = HttpContext.Request.Cookies["token"];
            string? cookieSessionId = HttpContext.Request.Cookies["session_id"];

            if (string.IsNullOrEmpty(sessionUsername) || string.IsNullOrEmpty(sessionToken) || string.IsNullOrEmpty(sessionSessionId) ||
                string.IsNullOrEmpty(cookieUsername) || string.IsNullOrEmpty(cookieToken) || string.IsNullOrEmpty(cookieSessionId) ||
                sessionUsername != cookieUsername || sessionToken != cookieToken || sessionSessionId != cookieSessionId)
            {
                TempData["ErrorMessage"] = "Authentication failed. Please log in.";
                return RedirectToPage("/Login");
            }

            // İlk yüklemede örnek veri oluştur
            if (ClassList.Count == 0)
            {
                for (int i = 1; i <= 100; i++)
                {
                    ClassList.Add(new ClassInformationModel
                    {
                        Id = i,
                        ClassName = $"Class {i}",
                        StudentCount = 20 + (i % 10),
                        Description = $"Auto-generated class #{i}"
                    });
                }
            }

            // Filtreleme ve sayfalama işlemleri
            var query = ClassList.AsQueryable();

            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(x => x.ClassName.Contains(Filter, System.StringComparison.OrdinalIgnoreCase));
            }

            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);
            DisplayClassList = query.ToList();

            PagedList = query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new ClassInformationTable
                {
                    Id = x.Id,
                    ClassName = x.ClassName,
                    StudentCount = x.StudentCount,
                    Description = x.Description
                })
                .ToList();

            return Page();
        }

        // --- Export Sadece Bu Sayfadaki Verileri JSON olarak indir ---
       public IActionResult OnGetExportCurrentPage()
{
    string? sessionUsername = HttpContext.Session.GetString("username");
    if (string.IsNullOrEmpty(sessionUsername))
    {
        return RedirectToPage("/Login", new { ErrorMessage = "You need to log in to export data." });
    }

    // PagedList'i tekrar oluşturuyoruz
    var query = ClassList.AsQueryable();

    if (!string.IsNullOrEmpty(Filter))
    {
        query = query.Where(x => x.ClassName.Contains(Filter, StringComparison.OrdinalIgnoreCase));
    }

    PagedList = query
        .Skip((PageNumber - 1) * PageSize)
        .Take(PageSize)
        .Select(x => new ClassInformationTable
        {
            Id = x.Id,
            ClassName = x.ClassName,
            StudentCount = x.StudentCount,
            Description = x.Description
        })
        .ToList();

    // JSON olarak dönüyoruz
    var json = Utils.Instance.ToJson(PagedList, null);
    return File(Encoding.UTF8.GetBytes(json), "application/json", "current_page_export.json");
}


        // --- Class Ekleme ---
        public IActionResult OnPostAddClass(string NewClassName, int NewStudentCount, string NewDescription)
        {
            int newId = ClassList.Any() ? ClassList.Max(c => c.Id) + 1 : 1;
            ClassList.Add(new ClassInformationModel
            {
                Id = newId,
                ClassName = NewClassName,
                StudentCount = NewStudentCount,
                Description = NewDescription
            });

            return RedirectToPage(new { PageNumber, Filter });
        }

        // --- Class Silme ---
        public IActionResult OnPostDeleteClass(int DeleteId)
        {
            var item = ClassList.FirstOrDefault(c => c.Id == DeleteId);
            if (item != null)
            {
                ClassList.Remove(item);
            }
            return RedirectToPage(new { PageNumber, Filter });
        }

        // --- Class Güncelleme ---
        public IActionResult OnPostEditClass(int EditId, string EditName, int EditStudentCount, string EditDescription)
        {
            var item = ClassList.FirstOrDefault(c => c.Id == EditId);
            if (item != null)
            {
                item.ClassName = EditName;
                item.StudentCount = EditStudentCount;
                item.Description = EditDescription;
            }

            return RedirectToPage(new { PageNumber, Filter });
        }
    }
}
