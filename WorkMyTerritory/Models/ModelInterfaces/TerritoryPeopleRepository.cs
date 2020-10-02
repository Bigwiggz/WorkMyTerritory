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
    public class TerritoryPeopleRepository : IGenericRepository<TerritoryPeople>, ITerritoryPeopleRepository
    {
        public readonly IConfiguration _configuration;
        public TerritoryPeopleRepository(IConfiguration configuration)
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
                    TerritoryPeopleId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDTerritoryPeople", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TerritoryPeople>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<TerritoryPeople>("spCRUDTerritoryPeople", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<TerritoryPeople> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {
                    TerritoryPeopleId = id
                };
                connection.Open();
                var affectedRows =await connection.QueryFirstOrDefaultAsync<TerritoryPeople>("spTerritoryPeople", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<TerritoryPeople>> GetTerPeoplebyHouseRecordsAsync(int HouseRecordsId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKHouseRecords = HouseRecordsId
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<TerritoryPeople>("spTerritoryPeople", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }

        public async void InsertAsync(TerritoryPeople obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    FKHouseRecords = obj.FKHouseRecords,
                    TerritoryPersonFirstName = obj.TerritoryPersonFirstName,
                    TerritoryPersonLastName = obj.TerritoryPersonLastName,
                    TerritoryPersonCellPhone = obj.TerritoryPersonCellPhone,
                    TerritoryPersonHomePhone1 = obj.TerritoryPersonHomePhone1,
                    TerritoryPersonHomePhone2 = obj.TerritoryPersonHomePhone2,
                    TerritoryPersonNotes = obj.TerritoryPersonNotes,
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDTerritoryPeople", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(TerritoryPeople obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    TerritoryPeopleId = obj.TerritoryPeopleId,
                    FKHouseRecords = obj.FKHouseRecords,
                    TerritoryPersonFirstName = obj.TerritoryPersonFirstName,
                    TerritoryPersonLastName = obj.TerritoryPersonLastName,
                    TerritoryPersonCellPhone = obj.TerritoryPersonCellPhone,
                    TerritoryPersonHomePhone1 = obj.TerritoryPersonHomePhone1,
                    TerritoryPersonHomePhone2 = obj.TerritoryPersonHomePhone2,
                    TerritoryPersonNotes = obj.TerritoryPersonNotes
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDCongregationTerritories", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
