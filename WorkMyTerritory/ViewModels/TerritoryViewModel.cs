using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;

namespace WorkMyTerritory.ViewModels
{
    public class TerritoryViewModel
    {
		public int CongregationTerritoriesId { get; set; }
		
		[Display(Name = "Territory Number")]
		public string TerritoryNumber { get; set; }
		
		[Display(Name = "Territory Special Notes")]
		public string TerritorySpecialNotes { get; set; }
		[Display(Name = "Territory Hidden Notes")]
		public string TerritoryHiddenNotes { get; set; }

		private string _TerritoryBoundariesText;
		[Display(Name = "Territory Boundaries Text")]
		public string TerritoryBoundariesText 
		{
			get 
			{
				//add geo wrapper
				string geoData = _TerritoryBoundariesText;
				string geoDataWrapperHead = "POLYGON((";
				//Might want to add a number switchout for other databases
				string geoDataWrapperTail = "))";
				string fullgeoData = geoDataWrapperHead + geoData + geoDataWrapperTail;
				//Parse to SqlGeography datatype
				return fullgeoData;
			}
			set
            {
				_TerritoryBoundariesText = value;

			} 
		}

		public SqlGeography TerritoryBoundaries { get; set; }

		[Display(Name = "Territory Type")]
		public EnumTerritoryType TerritorySpclTypes { get; set; }
		[Required]
		public int FKtblCongregationId { get; set; }
		public int? FKServiceGroup { get; set; }

		[DataType(DataType.Date)]
		private DateTime _LastUpdated;
		public DateTime LastUpdated 
		{ 
			get
            {
				return DateTime.Now;
            }
			set 
			{
				_LastUpdated = value;
			} 
		}
		[Display(Name = "Is Territory Active?")]
		public EnumRecordStatus TerritoryActive { get; set; }

		public int NumberofSpanish { get; }
	}
}
