using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.ViewModels
{
    public class ServiceGroupViewModel
    {
        [Required]
        public int ServiceGroupsId { get; set; }
        [Required]
        public int FKCongregationId { get; set; }
        [Required]
        [Display(Name = "Service Group Name")]
        public string ServiceGroupName { get; set; }
        [Display(Name = "Service Group Notes")]
        public string ServiceGroupNotes { get; set; }

        public EnumRecordStatus ServiceGroupActive { get; set; }
    }
}
