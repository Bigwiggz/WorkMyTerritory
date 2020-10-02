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
    public class CongregationTerritoriesRepository : IGenericRepository<CongregationTerritories>, ICongregationTerritoriesRepository
    {
        public readonly IConfiguration _configuration;
        public CongregationTerritoriesRepository(IConfiguration configuration)
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
                await connection.ExecuteAsync ("spCRUDCongregationTerritories", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<CongregationTerritories>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<CongregationTerritories>("spCRUDCongregationTerritories", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<CongregationTerritories> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    CongregationTerritoriesId = id
                };
                connection.Open();
                var affectedRows = await connection.QueryFirstOrDefaultAsync<CongregationTerritories>("spCongregationTerritories", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<CongregationTerritories>> GetCongTerrbyCongAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKtblCongregationId=id
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<CongregationTerritories>("spCongregationTerritories", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }

        public async void InsertAsync(CongregationTerritories obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    TerritoryNumber = obj.TerritoryNumber,
                    TerritorySpecialNotes = obj.TerritorySpecialNotes,
                    TerritoryHiddenNotes = obj.TerritoryHiddenNotes,
                    TerritoryBoundariesText = obj.TerritoryBoundariesText,
                    TerritorySpclTypes = obj.TerritorySpclTypes,
                    FKtblCongregationId = obj.FKtblCongregationId,
                    FKServiceGroup = obj.FKServiceGroup,
                    TerritoryActive = obj.TerritoryActive
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCongregationTerritories", insertParam, commandType: CommandType.StoredProcedure);
            }
        }
        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(CongregationTerritories obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    CongregationTerritoriesId = obj.CongregationTerritoriesId,
                    TerritoryNumber = obj.TerritoryNumber,
                    TerritorySpecialNotes = obj.TerritorySpecialNotes,
                    TerritoryHiddenNotes = obj.TerritoryHiddenNotes,
                    TerritoryBoundariesText = obj.TerritoryBoundariesText,
                    TerritorySpclTypes = obj.TerritorySpclTypes,
                    FKtblCongregationId = obj.FKtblCongregationId,
                    FKServiceGroup = obj.FKServiceGroup,
                    TerritoryActive = obj.TerritoryActive,

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCongregationTerritories", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
