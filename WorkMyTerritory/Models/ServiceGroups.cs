using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.Models
{
    public class ServiceGroups
    {
        public int ServiceGroupsId { get; set; }
        public int FKCongregationId { get; set; }
        public string ServiceGroupName { get; set; }
        public string ServiceGroupNotes { get; set; }
        public EnumRecordStatus ServiceGroupActive { get; set; }
    }
}
