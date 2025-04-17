// Models/ClassInformationTable.cs
namespace LabProject.Models
{
    public class ClassInformationTable
    {
        public string ClassName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public string Description { get; set; } = string.Empty;
        // Id gösterilmeyecek
        public int Id { get; set; }
    }
}