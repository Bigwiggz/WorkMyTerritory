using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface IUnitofWorkSample : IDisposable
    {
        ICampaignSpecialEventsRepository CampaignSpecialEvents { get; }
        ICongregationRepository Congregation { get; }
        ICongregationTerritoriesRepository CongregationTerritories { get; }
        IHouseRecordsRepository HouseRecords { get; }
        IPublishersRepository Publishers { get; }
        IServiceGroupsRepository ServiceGroups { get; }
        ITerritoryPeopleRepository TerritoryPeople { get; }
        ITerritoryWorkAssignmentRepository TerritoryWorkAssignment { get; }
        int Complete();
    }
}
