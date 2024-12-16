
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseProcedureController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BaseProcedureController(AppDbContext context)
        {
            _context = context;
        }

        protected async Task<ActionResult<IEnumerable<T>>> ExecuteProcedure<T>(string procedureName)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
        }
    }
}
