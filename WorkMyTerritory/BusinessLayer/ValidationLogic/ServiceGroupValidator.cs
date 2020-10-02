using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory.Models.ValidationLogic
{
    public class ServiceGroupValidator: AbstractValidator<ServiceGroupViewModel>
    {
        private readonly IEnumerable<ServiceGroups> _serviceGroupList;

        public ServiceGroupValidator(IEnumerable<ServiceGroups> serviceGroupList)
        {
            _serviceGroupList = serviceGroupList;

            //Service Group Congregation Name
            RuleFor(c => c.ServiceGroupName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Service Group Name Required")
                .MaximumLength(20).WithMessage("Service Group Name must not exceed 20 characters")
                .Must(BeUniqueServiceGroup).WithMessage("Service Group Name already exists.  Please enter in a new name.");

            //Service Group Notes
            RuleFor(c => c.ServiceGroupNotes).Length(150)
                .WithMessage("Service Group Name must not exceed 150 characters");

            //
        }

        protected bool BeUniqueServiceGroup(ServiceGroupViewModel serviceGroupEntry, string serviceGroupName)
        {
            //Find unique entry for Entry
            string serviceGroupUniqueEntry = serviceGroupEntry.ServiceGroupName=
                serviceGroupEntry.FKCongregationId+
                serviceGroupEntry.ServiceGroupActive.ToString();

            bool IsServiceGroupUniqueRecord = true;
            //Compare to all unique Entries
            foreach(ServiceGroups group in _serviceGroupList)
            {
                string serviceGroupUniqueRecords = group.ServiceGroupName +
                        group.FKCongregationId +
                        group.ServiceGroupActive.ToString();
                if(serviceGroupUniqueEntry==serviceGroupUniqueRecords)
                {
                    IsServiceGroupUniqueRecord = false;
                }

            }

            return IsServiceGroupUniqueRecord;
        }
    }
}
