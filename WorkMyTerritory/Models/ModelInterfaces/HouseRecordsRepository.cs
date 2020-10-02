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
    public class HouseRecordsRepository : IGenericRepository<HouseRecords>, IHouseRecordsRepository
    {
        public readonly IConfiguration _configuration;
        public HouseRecordsRepository(IConfiguration configuration)
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
                    HouseRecordsId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDHouseRecords", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<HouseRecords>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<HouseRecords>("spCRUDHouseRecords", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<HouseRecords> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    HouseRecordsId = id
                };
                connection.Open();
                var affectedRows =await connection.QueryFirstOrDefaultAsync<HouseRecords>("spHouseRecords", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<HouseRecords>> GetHouseRecordsbyTerrAsync(int CongregationId, int CongregationTerritoriesId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKCongregationId = CongregationId,
                    FKCongregationTerritoriesId= CongregationTerritoriesId
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<HouseRecords>("spHouseRecords", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }

        public async void InsertAsync(HouseRecords obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    HouseStreetNumber = obj.HouseStreetNumber,
                    HouseStreetAddress = obj.HouseStreetAddress,
                    AptLotNumber = obj.AptLotNumber,
                    City = obj.City,
                    USState = obj.USState,
                    ZIPCode = obj.ZIPCode,
                    HouseNotes = obj.HouseNotes,
                    HouseVisitSelection = obj.HouseVisitSelection,
                    HouseLatitude = obj.HouseLatitude,
                    HouseLongitude = obj.HouseLongitude,
                    HouseTypeSelection = obj.HouseTypeSelection,
                    HouseForeignLanguageSelection = obj.HouseForeignLanguageSelection,
                    FKCongregationId = obj.FKCongregationId,
                    FKCongregationTerritoriesId = obj.FKCongregationTerritoriesId,
                    HouseLastUpdated = obj.HouseLastUpdated,

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDHouseRecords", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(HouseRecords obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    HouseRecordsId = obj.HouseRecordsId,
                    HouseStreetNumber = obj.HouseStreetNumber,
                    HouseStreetAddress = obj.HouseStreetAddress,
                    AptLotNumber = obj.AptLotNumber,
                    City = obj.City,
                    USState = obj.USState,
                    ZIPCode = obj.ZIPCode,
                    HouseNotes = obj.HouseNotes,
                    HouseVisitSelection = obj.HouseVisitSelection,
                    HouseLatitude = obj.HouseLatitude,
                    HouseLongitude = obj.HouseLongitude,
                    HouseTypeSelection = obj.HouseTypeSelection,
                    HouseForeignLanguageSelection = obj.HouseForeignLanguageSelection,
                    FKCongregationId = obj.FKCongregationId,
                    FKCongregationTerritoriesId = obj.FKCongregationTerritoriesId,
                    HouseLastUpdated = obj.HouseLastUpdated
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDHouseRecords", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
