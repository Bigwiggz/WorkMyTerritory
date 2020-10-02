using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.BusinessLayer;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class CongregationRepository : IGenericRepository<Congregation>, ICongregationRepository
    {
        public readonly IConfiguration _configuration;
        public CongregationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void DeleteAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var deleteParam = new
                {
                    Action = "DELETE",
                    CongregationId = id
                };
                connection.Open();
                connection.Execute("spCRUDCongregation", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Congregation>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<Congregation>("spCRUDCongregation", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<Congregation> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    CongregationId = id
                };
                connection.Open();
                var affectedRows = await connection.QueryFirstOrDefaultAsync<Congregation>("spCongregation", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public IEnumerable<Congregation> GetCongregationSpecialSelectAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKCongregationId = id
                };
                connection.Open();
                var affectedRows = connection.Query<Congregation>("spCongregation", selectParam, commandType: CommandType.StoredProcedure).ToList();
                return affectedRows;
            }
        }

        public void InsertAsync(Congregation obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                 var insertParam = new
                {
                    Action = "INSERT",
                    CongregationName = obj.CongregationName,
                    CongregationStreetAddress = obj.CongregationStreetAddress,
                    CongregationCity=obj.CongregationCity,
                    CongregationState = obj.CongregationState,
                    CongregationZIPCode = obj.CongregationZIPCode,
                    CongregationLanguage = obj.CongregationLanguage,
                    CongregationLatitude = obj.CongregationLatitude,
                    CongregationLongitude = obj.CongregationLongitude,
                    CongregationNumber = obj.CongregationNumber,
                    CongregationActive = obj.CongregationActive
                };

                connection.Open();
                connection.Execute("spCRUDCongregation", insertParam, commandType: CommandType.StoredProcedure);
            }
        }
        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Congregation obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    CongregationId=obj.CongregationId,
                    CongregationName = obj.CongregationName,
                    CongregationStreetAddress = obj.CongregationStreetAddress,
                    CongregationCity = obj.CongregationCity,
                    CongregationState = obj.CongregationState,
                    CongregationZIPCode = obj.CongregationZIPCode,
                    CongregationLanguage = obj.CongregationLanguage,
                    CongregationLatitude = obj.CongregationLatitude,
                    CongregationLongitude = obj.CongregationLongitude,
                    CongregationNumber = obj.CongregationNumber,
                    CongregationActive = obj.CongregationActive
                };
                connection.Open();
                connection.Execute("spCRUDCongregation", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
