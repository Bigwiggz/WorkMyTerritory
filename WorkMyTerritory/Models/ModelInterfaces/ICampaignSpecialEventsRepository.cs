using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface ICampaignSpecialEventsRepository :IGenericRepository<CampaignSpecialEvents>
    {
        Task<IEnumerable<CampaignSpecialEvents>> GetCampaignsSpecialEventbyCongAsync(int id);
    }
}
