using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;

namespace WorkMyTerritory.ViewModels
{
    public class PublisherServiceGroupViewModel
    {
        [Required]
        public int ServiceGroupsId { get; set; }
        public IEnumerable<ServiceGroups> ServiceGroups { get; set; }

        [Required]
        public int Id { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }
    }
}
