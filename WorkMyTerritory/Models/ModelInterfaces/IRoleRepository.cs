using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    interface IRoleRepository<ApplicationRole>
    {
        public Task<IEnumerable<ApplicationRole>> GetAllRolesAsync();

        public Task<IEnumerable<ApplicationRole>> GetAllRolesCongregtionLevelandBelow();
    }
}
