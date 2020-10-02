using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.Enums;
using WorkMyTerritory.Models.ModelExtentions;

namespace WorkMyTerritory.Models
{
    public class CongregationTerritories
    {
		public int CongregationTerritoriesId { get; set; }
		public string TerritoryNumber { get; set; }
		public string TerritorySpecialNotes { get; set; }
		public string TerritoryHiddenNotes { get; set; }
		
		public SqlGeography TerritoryBoundaries { get; set; }

		public string TerritoryBoundariesText { get; set; }

		public double TerritoryArea { get; set; }

		public double TerritoryPerimeter { get; set; }

		//To handle the RecordStatus enum
		[Column("EnumTerritoryType")]
		public string TerritorySpclTypes
		{
			get { return EnumTerritoryType.ToString(); }
			private set { EnumTerritoryType = value.ParseEnum<EnumTerritoryType>(); }
		}
		[NotMapped]
		public EnumTerritoryType EnumTerritoryType { get; set; }

		public int FKtblCongregationId { get; set; }
		public int? FKServiceGroup { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime LastUpdated { get; set; }

		//To handle the RecordStatus enum
		[Column("EnumRecordStatus")]
		public string TerritoryActive
		{
			get { return EnumRecordStatus.ToString(); }
			private set { EnumRecordStatus = value.ParseEnum<EnumRecordStatus>(); }
		}
		[NotMapped]
		public EnumRecordStatus EnumRecordStatus { get; set; }
	}
}
