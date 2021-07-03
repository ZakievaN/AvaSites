using System.ComponentModel.DataAnnotations;

namespace AvailabilitySites.ViewModels
{
    public class SiteViewModel
    {
        [Display(Name = "Идентификатор")]
        public int PrimaryKey { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "Имя сайта")]
        public string Name { get; set; }

        [Required, Url]
        [Display(Name = "Адрес URL")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Интервал проверки сайта")]
        public int Interval { get; set; }
    }
}
