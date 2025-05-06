using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabProject.Data;
using LabProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Pages.Classes
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        public IList<Class> ClassList { get; set; } = new List<Class>();

        public async Task OnGetAsync()
        {
            ClassList = await _context.Classes.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddClassAsync(string NewClassName, int NewStudentCount, string NewDescription)
        {
            var newClass = new Class
            {
                Name = NewClassName,
                StudentCount = NewStudentCount,
                Description = NewDescription,
                IsActive = true
            };

            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteClassAsync(int DeleteId)
        {
            var cls = await _context.Classes.FindAsync(DeleteId);
            if (cls != null)
            {
                _context.Classes.Remove(cls);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditClassAsync(int EditId, string EditName, int EditStudentCount, string EditDescription)
        {
            var cls = await _context.Classes.FindAsync(EditId);
            if (cls != null)
            {
                cls.Name = EditName;
                cls.StudentCount = EditStudentCount;
                cls.Description = EditDescription;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
