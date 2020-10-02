using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class EmailTerritory
    {
        public string PublisherFirstName { get; set; }
        public string PublisherLastName { get; set; }

        private string _PublisherFullName;
        public string PublisherFullName 
        {
            get 
            {
                return $"{PublisherLastName}, {PublisherFirstName}";
            }
            set
            {
                _PublisherFullName = value;
            }
        }
        public string Email { get; set; }
    }
}
