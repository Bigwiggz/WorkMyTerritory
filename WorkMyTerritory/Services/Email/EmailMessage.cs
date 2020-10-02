using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.ModelInterfaces;

namespace WorkMyTerritory.Models
{
    public class EmailMessage
    {
		public EmailMessage()
		{
			ToAddresses = new List<EmailTerritory>();
			FromAddresses = new List<EmailTerritory>();
		}

		public List<EmailTerritory> ToAddresses { get; set; }
		public List<EmailTerritory> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}
