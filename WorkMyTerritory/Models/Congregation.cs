using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.Models
{
    public class Congregation
    {
		public int Id { get; set; }
		public int CongregationId { get; set; }

		[Display(Name = "Name of Congregation")]
		public string CongregationName { get; set; }

		[Display(Name = "Congregation Street Address")]
		public string CongregationStreetAddress { get; set; }

		[Display(Name = "Congregation City")]
		public string CongregationCity { get; set; }

		[Display(Name = "State")]
		public string CongregationState { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 3)]
		[Display(Name = "Congregation Zip Code")]
		public string CongregationZIPCode { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 2)]
		[Display(Name = "Language of Congregation")]
		public string CongregationLanguage { get; set; }
		[Column(TypeName = "decimal(9, 6)")]
		[Display(Name = "Latitude")]
		public decimal CongregationLatitude { get; set; }
		[Column(TypeName = "decimal(9, 6)")]
		[Display(Name = "Longitude")]
		public decimal CongregationLongitude { get; set; }
        [RegularExpression(@"^[0-9]{1,6}$")]
		[Display(Name = "Congregation Number")]
		public string CongregationNumber { get; set; }
		public EnumRecordStatus CongregationActive { get; set; }
	}
}
