using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvailabilitySites.ViewModels
{
    public class SiteAvailableViewModel
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
        [Display(Name = "Интервал проверки доступности")]
        public int Interval { get; set; }

        [Display(Name = "Доступность")]
        public bool IsAvailable { get; set; }
    }
}
