using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class ClassInformationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sınıf Adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Sınıf Adı 100 karakteri geçemez.")]
        public string ClassName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci Sayısı gereklidir.")]
        [Range(0, int.MaxValue, ErrorMessage = "Öğrenci Sayısı negatif olamaz.")]
        public int StudentCount { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama 500 karakteri geçemez.")]
        public string Description { get; set; } = string.Empty;
    }
}