using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface IServiceGroupsRepository:IGenericRepository<ServiceGroups>
    {
        public Task<IEnumerable<ServiceGroups>> GetServiceGroupsbyCongAsync(int Congregationid);

    }
}
