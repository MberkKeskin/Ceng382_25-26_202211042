using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using LabProject.Models;

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

        public List<ClassInformationModel> DisplayClassList { get; set; } = new List<ClassInformationModel>();

        public void OnGet(int? id = null)
        {
            DisplayClassList = ClassList.ToList();

            if (id.HasValue && ClassList.Any(c => c.Id == id))
            {
                EditId = id;
                NewClass = ClassList.FirstOrDefault(c => c.Id == id) ?? new ClassInformationModel();
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

            NewClass.Id = nextId++; // Yeni sınıf eklerken ID'yi al ve artır
            ClassList.Add(NewClass);
            return RedirectToPage("./Index", new { id = (int?)null });
        }

       public IActionResult OnPostDelete(int id)
{
    var item = ClassList.FirstOrDefault(c => c.Id == id);
    if (item != null)
    {
        ClassList.Remove(item);

        // ID'yi sıfırla ve listeyi yeniden düzenle
        ReassignIds();
    }

    // Silme işlemi sonrasında yeni sınıf eklemek için yönlendir
    return RedirectToPage("./Index", new { id = (int?)null });
}


        // ID'leri yeniden sıralamak için kullanılan yardımcı metod
        private void ReassignIds()
        {
            // ID'leri sıfırdan yeniden düzenle
            var counter = 1;
            foreach (var item in ClassList)
            {
                item.Id = counter++;
            }

            // En son kullanılan ID'yi güncelle
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

            return RedirectToPage("./Index", new { id = (int?)null }); // Edit sonrası yeni ekleme moduna dön
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./Index", new { id = (int?)null }); // İptal edince edit modunu kapat
        }
    }
}
