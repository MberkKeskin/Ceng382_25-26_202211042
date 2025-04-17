// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabProject.Models;
using LabProject.Helpers;
using System.Linq;
using System;
using System.Text;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
        public static List<ClassInformationModel> ClassList { get; set; } = new();
        public static List<ClassInformationModel> DisplayClassList { get; set; } = new();

        public List<ClassInformationTable> PagedList { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }
        public const int PageSize = 10;

        public void OnGet()
        {
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

            var query = ClassList.AsQueryable();

            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(x => x.ClassName.Contains(Filter, StringComparison.OrdinalIgnoreCase));
            }

            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);
            DisplayClassList = query.ToList();

            PagedList = query.Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .Select(x => new ClassInformationTable
                        {
                            Id = x.Id,
                            ClassName = x.ClassName,
                            StudentCount = x.StudentCount,
                            Description = x.Description
                        }).ToList();
        }

        public IActionResult OnGetExportJson(bool filtered = false, string? columns = null)
        {
            var selectedColumns = string.IsNullOrEmpty(columns) ? null : columns.Split(',').ToList();
            var data = filtered ? DisplayClassList : ClassList;
            var json = Utils.Instance.ToJson(data, selectedColumns);
            return File(Encoding.UTF8.GetBytes(json), "application/json", "export.json");
        }
    }
}