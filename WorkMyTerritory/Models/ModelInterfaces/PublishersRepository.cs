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
    public class PublishersRepository : IGenericRepository<Publishers>, IPublishersRepository
    {
        public readonly IConfiguration _configuration;
        public PublishersRepository(IConfiguration configuration)
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
                    PublisherId = id
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDPublishers", deleteParam, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Publishers>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    Action = "SELECT"
                };
                connection.Open();
                var affectedRows = await connection.QueryAsync<Publishers>("spCRUDPublishers", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            }
        }

        public async Task<Publishers> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectbyIdParam = new
                {

                    PublisherId = id
                };
                connection.Open();
                var affectedRows =await connection.QueryFirstOrDefaultAsync<Publishers>("spPublishers", selectbyIdParam, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<IEnumerable<Publishers>> GetPublishersbyCongAsync(int Congregationid)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var selectParam = new
                {
                    FKPublisherCongregationId = Congregationid
                };
                connection.Open();
                var affectedRows =await connection.QueryAsync<Publishers>("spPublishers", selectParam, commandType: CommandType.StoredProcedure);
                return affectedRows.ToList();
            };
        }

        public async void InsertAsync(Publishers obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var insertParam = new
                {
                    Action = "INSERT",
                    PublisherFirstName = obj.PublisherFirstName,
                    PublisherLastName = obj.PublisherLastName,
                    PublisherAppointment = obj.PublisherAppointment,
                    PublisherSex = obj.PublisherSex,
                    PublisherPrivileges = obj.PublisherPrivileges,
                    PublisherPhoneNumber = obj.PublisherPhoneNumber,
                    PublisherEmail = obj.PublisherEmail,
                    FKPublisherCongregation = obj.FKPublisherCongregation,
                    FKFieldServiceGroup = obj.FKFieldServiceGroup,
                    PublisherActive = obj.PublisherActive,
                    TerritoryGroupAssigner = obj.TerritoryGroupAssigner,

                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDPublishers", insertParam, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(Publishers obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnString")))
            {
                var updateParam = new
                {
                    Action = "UPDATE",
                    PublisherId = obj.PublisherId,
                    PublisherFirstName = obj.PublisherFirstName,
                    PublisherLastName = obj.PublisherLastName,
                    PublisherAppointment = obj.PublisherAppointment,
                    PublisherSex = obj.PublisherSex,
                    PublisherPrivileges = obj.PublisherPrivileges,
                    PublisherPhoneNumber = obj.PublisherPhoneNumber,
                    PublisherEmail = obj.PublisherEmail,
                    FKPublisherCongregation = obj.FKPublisherCongregation,
                    FKFieldServiceGroup = obj.FKFieldServiceGroup,
                    PublisherActive = obj.PublisherActive,
                    TerritoryGroupAssigner = obj.TerritoryGroupAssigner
                };
                connection.Open();
                await connection.ExecuteAsync("spCRUDPublishers", updateParam, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
