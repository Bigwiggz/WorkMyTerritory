using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface ITerritoryPeopleRepository:IGenericRepository<TerritoryPeople>
    {
        Task<IEnumerable<TerritoryPeople>> GetTerPeoplebyHouseRecordsAsync(int HouseRecordsId);
    }
}
