using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

//Change Congregation Spelling in DB
//Figure out return type Generic<> for Dapper Execute, Query
//What is Save used for?

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class CampaignSpecialEventsRepository : IGenericRepository<CampaignSpecialEvents>, ICampaignSpecialEventsRepository
    {
        public readonly IConfiguration _configuration;
        public CampaignSpecialEventsRepository(IConfiguration configuration)
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
                    SpecialCampaignId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCampaignSpecialEvents", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<CampaignSpecialEvents>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<CampaignSpecialEvents>("spCRUDCampaignSpecialEvents", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<CampaignSpecialEvents> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    SpecialCampaignId = id
                };
                connection.Open();
                var affectedRows= await connection.QueryFirstOrDefaultAsync<CampaignSpecialEvents>("spCampaignSpecialEvents", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<CampaignSpecialEvents>> GetCampaignsSpecialEventbyCongAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKCongregationId = id
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<CampaignSpecialEvents>("spCampaignSpecialEvents", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async void InsertAsync(CampaignSpecialEvents obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    SpecialCampaignName=obj.SpecialCampaignName,
                    SpecialCampaignDescription=obj.SpecialCampaignDescription,
                    SpecialCampaignStartDate=obj.SpecialCampaignStartDate,
                    SpecialCampaignEndDate=obj.SpecialCampaignEndDate,
                    FKCongregationId = obj.FKCongregationId
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCampaignSpecialEvents", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public  void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(CampaignSpecialEvents obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    SpecialCampaignId = obj.SpecialCampaignId,
                    SpecialCampaignName = obj.SpecialCampaignName,
                    SpecialCampaignDescription = obj.SpecialCampaignDescription,
                    SpecialCampaignStartDate = obj.SpecialCampaignStartDate,
                    SpecialCampaignEndDate = obj.SpecialCampaignEndDate,
                    FKCongregationId = obj.FKCongregationId
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCampaignSpecialEvents", updateParam, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
