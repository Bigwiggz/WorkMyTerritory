using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models
{
    public class HouseRecords
    {
		public long HouseRecordsId { get; set; }
		public string HouseStreetNumber { get; set; }
		public string HouseStreetAddress { get; set; }
		public string AptLotNumber { get; set; }
		public string City { get; set; }
		public string USState { get; set; }
		public string ZIPCode { get; set; }
		public string HouseNotes { get; set; }
		public string HouseVisitSelection { get; set; }
		public decimal HouseLatitude { get; set; }
		public decimal HouseLongitude { get; set; }
		public string HouseTypeSelection { get; set; }
		public string HouseForeignLanguageSelection { get; set; }
		public int FKCongregationId { get; set; }
		public int FKCongregationTerritoriesId { get; set; }
		[DataType(DataType.Date)]
		public DateTime HouseLastUpdated { get; set; }
	}
}
