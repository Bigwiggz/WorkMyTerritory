using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class ServiceGroupsRepository : IGenericRepository<ServiceGroups>, IServiceGroupsRepository
    {
        public readonly IConfiguration _configuration;
        public ServiceGroupsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async void DeleteAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var deleteParam = new
                {
                    Action = "DELETE",
                    CongregationTerritoriesId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDServiceGroups", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ServiceGroups>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<ServiceGroups>("spCRUDServiceGroups", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<ServiceGroups> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    ServiceGroupsId = id
                };
                connection.Open();
                var affectedRows =await connection.QueryFirstOrDefaultAsync<ServiceGroups>("spServiceGroups", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<ServiceGroups>> GetServiceGroupsbyCongAsync(int Congregationid)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKCongregationId = Congregationid
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<ServiceGroups>("spServiceGroups", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }

        public async void InsertAsync(ServiceGroups obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    FKCongregationId = obj.FKCongregationId,
                    ServiceGroupName = obj.ServiceGroupName,
                    ServiceGroupNotes = obj.ServiceGroupNotes,

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDServiceGroups", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(ServiceGroups obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    ServiceGroupsId = obj.ServiceGroupsId,
                    FKCongregationId = obj.FKCongregationId,
                    ServiceGroupName = obj.ServiceGroupName,
                    ServiceGroupNotes = obj.ServiceGroupNotes
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDServiceGroups", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
