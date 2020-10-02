using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface IPublishersRepository:IGenericRepository<Publishers>
    {
        public Task<IEnumerable<Publishers>> GetPublishersbyCongAsync(int Congregationid);
    }
}
