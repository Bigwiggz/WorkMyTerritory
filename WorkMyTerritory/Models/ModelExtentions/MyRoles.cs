using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models.ModelInterfaces;

namespace WorkMyTerritory.Models.ModelExtentions
{
    public class MyRoles : RoleManager<ApplicationRole>, IRoleRepository<ApplicationRole>
    {
        private readonly IConfiguration _configuration;

        public MyRoles(IConfiguration configuration, IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<ApplicationRole>> GetAllRolesAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var queryResults = await connection.QueryAsync<ApplicationRole>("SELECT * FROM [ApplicationRole]");

                return queryResults.ToList();
            }
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllRolesCongregtionLevelandBelow()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var queryResults = await connection.QueryAsync<ApplicationRole>("SELECT * FROM [ApplicationRole]" +
                    "WHERE Id<>1");

                return queryResults.ToList();
            }
        }
    }
}
