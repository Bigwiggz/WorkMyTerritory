using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models
{
    public class Publishers
    {
		public int PublisherId { get; set; }
		public string PublisherFirstName { get; set; }
		public string PublisherLastName { get; set; }
		public string PublisherAppointment { get; set; }
		public string PublisherSex { get; set; }
		public string PublisherPrivileges { get; set; }
		public string PublisherPhoneNumber { get; set; }
		public string PublisherEmail { get; set; }
		public int FKPublisherCongregation { get; set; }
		public int FKFieldServiceGroup { get; set; }
		public string PublisherActive { get; set; }
		public string TerritoryGroupAssigner { get; set; }
	}
}
