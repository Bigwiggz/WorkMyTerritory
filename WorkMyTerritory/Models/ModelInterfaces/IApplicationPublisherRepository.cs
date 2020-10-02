using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    interface IApplicationPublisherRepository<ApplicationUser>
    {
        public Task<IEnumerable<ApplicationUser>> GetApplicationUsersbyCongregation(int CongregationId);
    };
}
