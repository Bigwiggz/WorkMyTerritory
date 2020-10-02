using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class UnitofWorkSample : IUnitofWorkSample
    {
        private readonly IConfiguration _configuration;
        public UnitofWorkSample(IConfiguration configuration)
        {
            _configuration = configuration;
            CampaignSpecialEvents = new CampaignSpecialEventsRepository(_configuration);
            Congregation = new CongregationRepository(_configuration);
            CongregationTerritories = new CongregationTerritoriesRepository(_configuration);
            HouseRecords = new HouseRecordsRepository(_configuration);
            Publishers = new PublishersRepository(_configuration);
            ServiceGroups = new ServiceGroupsRepository(_configuration);
            TerritoryPeople = new TerritoryPeopleRepository(_configuration);
            TerritoryWorkAssignment = new TerritoryWorkAssignmentRepository(_configuration);
        }
        public ICampaignSpecialEventsRepository CampaignSpecialEvents { get; private set; }

        public ICongregationRepository Congregation { get; private set; }

        public ICongregationTerritoriesRepository CongregationTerritories { get; private set; }

        public IHouseRecordsRepository HouseRecords { get; private set; }

        public IPublishersRepository Publishers { get; private set; }

        public IServiceGroupsRepository ServiceGroups { get; private set; }

        public ITerritoryPeopleRepository TerritoryPeople { get; private set; }

        public ITerritoryWorkAssignmentRepository TerritoryWorkAssignment { get; private set; }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
