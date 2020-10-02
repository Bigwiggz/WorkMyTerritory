using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models
{
    public class TerritoryPeople
    {
		public long TerritoryPeopleId { get; set; }
		public long FKHouseRecords { get; set; }
		public string TerritoryPersonFirstName { get; set; }
		public string TerritoryPersonLastName { get; set; }
		public string TerritoryPersonCellPhone { get; set; }
		public string TerritoryPersonHomePhone1 { get; set; }
		public string TerritoryPersonHomePhone2 { get; set; }
		public string TerritoryPersonNotes { get; set; }
	}
}
