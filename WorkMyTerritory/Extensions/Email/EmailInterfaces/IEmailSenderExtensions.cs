using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;
using WorkMyTerritory.Services.Email.BaseInterfaces;

namespace WorkMyTerritory.Extensions.Email.EmailInterfaces
{
    public interface IEmailSenderExtensions
    {
        public Task SendEmailConfirmationAsync(ApplicationUser user, string link);

        public Task SendTerritoryAssignmentEmailAsync(TerritoryWorkAssignment territoryAssignment, string link);


    }
}
