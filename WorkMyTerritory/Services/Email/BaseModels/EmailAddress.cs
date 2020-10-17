using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Services.Email.BaseModels
{
    public class EmailAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string _FullName;
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }

        }
        public string Email { get; set; }
    }
}
