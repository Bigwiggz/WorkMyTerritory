using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkMyTerritory.ViewModels
{
    public class CampaignAddSpecialEventsViewModel
    {
		public string SpecialCampaignName { get; set; }
		public string SpecialCampaignDescription { get; set; }
		[DataType(DataType.Date)]
		public DateTime SpecialCampaignStartDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime SpecialCampaignEndDate { get; set; }
		public int FKCongregationId { get; set; }
	}
}
