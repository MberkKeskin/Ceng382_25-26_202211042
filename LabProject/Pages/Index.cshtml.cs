using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabProject.Models;
using Microsoft.EntityFrameworkCore;
using LabProject.Data;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        public List<Class> PagedList { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }
        public const int PageSize = 10;

        public IActionResult OnGet()
        {
            IQueryable<Class> query = _context.Classes.Where(c => c.IsActive);

            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(x => x.Name.Contains(Filter));
            }

            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);

            PagedList = query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }

        public IActionResult OnPostAddClass(string NewClassName, int NewStudentCount, string NewDescription)
        {
            var newClass = new Class
            {
                Name = NewClassName,
                StudentCount = NewStudentCount,
                Description = NewDescription,
                IsActive = true
            };

            _context.Classes.Add(newClass);
            _context.SaveChanges();

            return RedirectToPage(new { PageNumber, Filter });
        }

        public IActionResult OnPostEditClass(int EditId, string EditName, int EditStudentCount, string EditDescription)
        {
            var item = _context.Classes.FirstOrDefault(c => c.Id == EditId);
            if (item != null)
            {
                item.Name = EditName;
                item.StudentCount = EditStudentCount;
                item.Description = EditDescription;
                _context.SaveChanges();
            }
            return RedirectToPage(new { PageNumber, Filter });
        }

        public IActionResult OnPostDeleteClass(int DeleteId)
        {
            var item = _context.Classes.FirstOrDefault(c => c.Id == DeleteId);
            if (item != null)
            {
                item.IsActive = false;
                _context.SaveChanges();
            }
            return RedirectToPage(new { PageNumber, Filter });
        }
    }
}
