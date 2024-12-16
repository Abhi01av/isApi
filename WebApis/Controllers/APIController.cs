

using Microsoft.AspNetCore.Mvc;
using WebApis.Data;
using WebApis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApis.Controllers
{
    [Route("api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public APIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CallRankBonusProcedure")]
        public async Task<ActionResult<IEnumerable<RankBonus>>> CallRankBonusProcedure()
        {
            return await ExecuteProcedure<RankBonus>("sp_GetAllRankBonuses");
        }

        [HttpPost("CallUserBonusProcedure")]
        public async Task<ActionResult<IEnumerable<RankBonus>>> CallUserBonusProcedure()
        {
            return await ExecuteProcedure<RankBonus>("sp_GetAllRankBonuses");
        }

        private async Task<ActionResult<IEnumerable<T>>> ExecuteProcedure<T>(string procedureName)
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

