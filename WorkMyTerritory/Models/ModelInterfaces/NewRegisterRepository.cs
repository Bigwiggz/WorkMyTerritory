using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public class NewRegisterRepository : INewRegisterRepository<Congregation, ApplicationUser>
    {
        public readonly IConfiguration _configuration;
        public NewRegisterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void InsertAsync(Congregation obj1, ApplicationUser obj2)
        {
        }
    }
}
