using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models
{
    public class TerritoryWorkAssignment
    {
		public long TerritoryWorkAssignmentId { get; set; }
		public int FKCongregationId { get; set; }
		public int FKPublisherId { get; set; }
		public int FKCongregationTerritoryId { get; set; }
		public int FKCampaignId { get; set; }
		public int FKFieldServiceGroupId { get; set; }
		[DataType(DataType.Date)]
		public DateTime TerritoryAssignmentStartDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime TerritoryAssignmentEndDate { get; set; }
		public string UniqueLinkKey { get; set; }
        public object TerritoryAssignmentLastUpdated { get; internal set; }
    }
}
