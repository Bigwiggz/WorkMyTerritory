using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface ITerritoryWorkAssignmentRepository:IGenericRepository<TerritoryWorkAssignment>
    {
        Task<IEnumerable<TerritoryWorkAssignment>> GetTerrbyCongAsync(int CongregationId);
    }
}
