using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.ViewModels
{
    public class CongregationPortalViewModel
    {
        public int CongregationId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Name of Congregation")]
        public string CongregationName { get; set; }

        public int Id { get; set; }

        public string PublisherFirstName { get; set; }

        public string PublisherLastName { get; set; }
    }
}
