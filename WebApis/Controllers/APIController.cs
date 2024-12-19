using Microsoft.AspNetCore.Mvc;
using WebApis.Data;
using WebApis.Models;
using WebApis.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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

        [HttpPost("NewUser")]
        public async Task<ActionResult<UserDetailsOutputModel>> NewUser([FromBody] NewUserInputModel newUser)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                var executor = new StoredProcedureExecutor(connection);

                try
                {
                    // Execute the stored procedure to insert the user and return the user details
                    var userDetails = await connection.QueryFirstOrDefaultAsync<UserDetailsOutputModel>(
                        "sp_NewUser",
                        newUser,
                        commandType: CommandType.StoredProcedure
                    );

                    if (userDetails == null)
                    {
                        return BadRequest("An error occurred while adding the user.");
                    }

                    return Ok(userDetails);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50000 && ex.Class == 16) // Error number and severity for RAISERROR
                    {
                        return BadRequest(ex.Message);
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while adding the user.");
                    }
                }
            }
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
