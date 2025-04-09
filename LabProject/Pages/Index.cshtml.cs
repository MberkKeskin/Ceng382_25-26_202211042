using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> ClassList { get; set; } = new List<ClassInformationModel>();
        private static int nextId = 1;

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new ClassInformationModel();

        [BindProperty]
        public int? EditId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public const int PageSize = 10;
        public int TotalPages { get; set; }

        public List<ClassInformationModel> DisplayClassList { get; set; } = new();

        public void OnGet(int? id = null)
        {
            // Filtre uygula
            var query = ClassList.AsQueryable();

            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(c => c.ClassName.Contains(Filter, System.StringComparison.OrdinalIgnoreCase));
            }

            // Sayfalama
            TotalPages = (int)System.Math.Ceiling(query.Count() / (double)PageSize);
            DisplayClassList = query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Edit
            if (id.HasValue && ClassList.Any(c => c.Id == id))
            {
                EditId = id;
                NewClass = ClassList.First(c => c.Id == id);
            }
            else
            {
                EditId = null;
                NewClass = new ClassInformationModel();
            }
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                DisplayClassList = ClassList.ToList();
                return Page();
            }

            NewClass.Id = nextId++;
            ClassList.Add(NewClass);
            return RedirectToPage("./Index", new { id = (int?)null });
        }

        public IActionResult OnPostDelete(int id)
        {
            var item = ClassList.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                ClassList.Remove(item);
                ReassignIds();
            }

            return RedirectToPage("./Index", new { id = (int?)null });
        }

        private void ReassignIds()
        {
            int counter = 1;
            foreach (var item in ClassList)
            {
                item.Id = counter++;
            }

            nextId = counter;
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingClass = ClassList.FirstOrDefault(c => c.Id == NewClass.Id);
            if (existingClass != null)
            {
                existingClass.ClassName = NewClass.ClassName;
                existingClass.StudentCount = NewClass.StudentCount;
                existingClass.Description = NewClass.Description;
            }

            return RedirectToPage("./Index", new { id = (int?)null });
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./Index", new { id = (int?)null });
        }
    }
}
