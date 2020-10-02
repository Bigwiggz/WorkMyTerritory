using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.ModelInterfaces;

namespace WorkMyTerritory.Models.MockRepository
{
    public class MockCampaignSpecialEventsRepository : ICampaignSpecialEventsRepository
    {
        private List<CampaignSpecialEvents> _CampaignSpecialEvents;

        public MockCampaignSpecialEventsRepository()
        {
            _CampaignSpecialEvents = new List<CampaignSpecialEvents>()
            {
                new CampaignSpecialEvents(){},
                new CampaignSpecialEvents(){},
                new CampaignSpecialEvents(){},
                new CampaignSpecialEvents(){}
            };
        }

        public void DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CampaignSpecialEvents>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CampaignSpecialEvents> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<CampaignSpecialEvents> GetCampaignSpecialEventAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CampaignSpecialEvents>> GetCampaignsSpecialEventbyCongAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertAsync(CampaignSpecialEvents obj)
        {
            throw new NotImplementedException();
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(CampaignSpecialEvents obj)
        {
            throw new NotImplementedException();
        }
    }
}
