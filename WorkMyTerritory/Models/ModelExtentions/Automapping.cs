using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Models.ModelExtentions
{
    public class Automapping:Profile
    {
        public Automapping()
        {
            //ServiceGroup Controller
            CreateMap<ServiceGroups, ServiceGroupViewModel>();

            //Congregation Controller
            CreateMap<Congregation, CongregationViewModel>();

            //Campaign Controller
            CreateMap<CampaignSpecialEvents,CampaignSpecialEventsViewModel>();
            CreateMap<CampaignSpecialEventsViewModel,CampaignSpecialEvents>();

            //Campaign view model without ID
            CreateMap<CampaignAddSpecialEventsViewModel,CampaignSpecialEvents>();

            //Publisher Controller Mapping
            CreateMap<ApplicationUser, PublisherViewModel>();
            CreateMap<PublisherViewModel,ApplicationUser>();

            //Territories Controller Mapping
            CreateMap<CongregationTerritories, TerritoryViewModel>();
            CreateMap<TerritoryViewModel, CongregationTerritories>();
        }
    }
}
