using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Models.ValidationLogic
{
    public class CampaignEditValidator: AbstractValidator<CampaignSpecialEventsViewModel>
    {
        private readonly IEnumerable<CampaignSpecialEventsViewModel> _campaigns;

        public CampaignEditValidator(IEnumerable<CampaignSpecialEventsViewModel> campaigns)
        {
            _campaigns = campaigns;

            //Campaign End Date Required and Start Date must be before end date
            RuleFor(c => c.SpecialCampaignEndDate).NotEmpty().WithMessage("Campaign End Date Required")
                .GreaterThanOrEqualTo(c => c.SpecialCampaignStartDate).WithMessage("Campaign Start Date must be before End Date")
                .When(c => c.SpecialCampaignStartDate!=null);

            //Campaign Start Date Required
            RuleFor(c => c.SpecialCampaignStartDate).NotEmpty().WithMessage("Campaign Start Date Required");

            //Campaign Name Required and must not exceeed 
            RuleFor(c => c.SpecialCampaignName).NotEmpty().WithMessage("Campaign Name Required")
                .Length(0,100).WithMessage("Campaign Name must not exceed 100 characters");

            //Check for unique value and, SpecialCampaignEndDate,SpecialCampaignStartDate,CampaignActive,FKCongregationID)
            RuleFor(c => c.SpecialCampaignName).Must(IsCampaignUnique)
                .WithMessage("Campaign Entered is not Unique.");
        }
        //Need to fix adding (SpecialCampaignName,SpecialCampaignEndDate,SpecialCampaignStartDate,CampaignActive,FKCongregationID)
        //Custom Validator
        public bool IsCampaignUnique(CampaignSpecialEventsViewModel editedCampaign, string newValue)
        {
            
            return _campaigns.All(campaign =>
              campaign.Equals(editedCampaign) || campaign.SpecialCampaignName != newValue);
        }
    }
}
