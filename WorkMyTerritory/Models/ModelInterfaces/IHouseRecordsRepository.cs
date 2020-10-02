using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface IHouseRecordsRepository:IGenericRepository<HouseRecords>
    {
        public Task<IEnumerable<HouseRecords>> GetHouseRecordsbyTerrAsync(int CongregationId, int CongregationTerritoriesId);
    }
}
