using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface ICongregationTerritoriesRepository : IGenericRepository<CongregationTerritories>
    {
        Task<IEnumerable<CongregationTerritories>> GetCongTerrbyCongAsync(int id);
    }
}
