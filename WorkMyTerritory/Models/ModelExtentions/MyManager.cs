using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkMyTerritory.Models.ModelInterfaces;

namespace WorkMyTerritory.Models.ModelExtentions
{
    public class MyManager : UserManager<ApplicationUser>, IApplicationPublisherRepository<ApplicationUser>
    {

        private readonly IConfiguration _configuration;

        public MyManager(IConfiguration configuration, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersbyCongregation(int CongregationId)
        {

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var queryResults = await connection.QueryAsync<ApplicationUser>("SELECT * FROM [ApplicationUser] " +
                    "WHERE FKPublisherCongregation=@FKCongregationId",
                    new { FKCongregationId = CongregationId });

                return queryResults.ToList();
            }
        }
    }
}
