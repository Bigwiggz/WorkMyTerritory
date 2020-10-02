using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface ICongregationRepository: IGenericRepository<Congregation>
    {
        IEnumerable<Congregation> GetCongregationSpecialSelectAsync(object id);
    }
}
