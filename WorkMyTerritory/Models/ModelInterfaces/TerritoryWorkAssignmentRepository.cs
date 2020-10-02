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
    public class TerritoryWorkAssignmentRepository : IGenericRepository<TerritoryWorkAssignment>, ITerritoryWorkAssignmentRepository
    {
        public readonly IConfiguration _configuration;
        public TerritoryWorkAssignmentRepository(IConfiguration configuration)
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
                    TerritoryWorkAssignmentId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDTerritoryWorkAssignment", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TerritoryWorkAssignment>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<TerritoryWorkAssignment>("spCRUDTerritoryWorkAssignment", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<TerritoryWorkAssignment> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    TerritoryWorkAssingmentId = id
                };
                connection.Open();
                var affectedRows =await connection.QueryFirstOrDefaultAsync<TerritoryWorkAssignment>("spTerritoryWorkAssignment", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<TerritoryWorkAssignment>> GetTerrbyCongAsync(int CongregationId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKCongregationTerritoryId = CongregationId
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<TerritoryWorkAssignment>("spTerritoryWorkAssignment", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }
        public async void InsertAsync(TerritoryWorkAssignment obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                DateTime GetCurrentDate = DateTime.Now;
                var insertParam = new
                {
                    Action = "INSERT",
                    FKCongregationId = obj.FKCongregationId,
                    FKPublisherId = obj.FKPublisherId,
                    FKCongregationTerritoryId = obj.FKCongregationTerritoryId,
                    FKCampaignId = obj.FKCampaignId,
                    FKFieldServiceGroupId = obj.FKFieldServiceGroupId,
                    TerritoryAssignmentStartDate = obj.TerritoryAssignmentStartDate,
                    TerritoryAssignmentEndDate = obj.TerritoryAssignmentEndDate,
                    UniqueLinkKey = obj.UniqueLinkKey,
                    TerritoryAssignmentLastUpdated = GetCurrentDate

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDTerritoryPeople", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(TerritoryWorkAssignment obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                DateTime GetCurrentDate = DateTime.Now;
                var updateParam = new
                {
                    Action = "UPDATE",
                    TerritoryWorkAssignmentId = obj.TerritoryWorkAssignmentId,
                    FKCongregationId = obj.FKCongregationId,
                    FKPublisherId = obj.FKPublisherId,
                    FKCongregationTerritoryId = obj.FKCongregationTerritoryId,
                    FKCampaignId = obj.FKCampaignId,
                    FKFieldServiceGroupId = obj.FKFieldServiceGroupId,
                    TerritoryAssignmentStartDate = obj.TerritoryAssignmentStartDate,
                    TerritoryAssignmentEndDate = obj.TerritoryAssignmentEndDate,
                    UniqueLinkKey = obj.UniqueLinkKey,
                    TerritoryAssignmentLastUpdated = GetCurrentDate

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDTerritoryWorkAssignment", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
