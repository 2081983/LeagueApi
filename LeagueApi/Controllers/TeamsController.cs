using LeagueApi.Dependency.IRepository;
using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository repo;

        public TeamsController(ITeamRepository _repo )
        {
            repo = _repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teams = await repo.Teamlist();

                return Ok(teams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetTeam/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            try
            {
                var team = await repo.GetTeam(id);
                if (team.Id == 0)
                {
                    return BadRequest(new { message = "no data has been found" });
                }   
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam(TeamDto model)
        {
            if (model == null || model.Name.IsNullOrEmpty())
            {
                return BadRequest(new { message = "No Data has been Sent" });
            }
             if(await checkname(model.Name))
            {
                return BadRequest(new { message = "team Name Already In Use" });
            }
            try
            {
                var team = await repo.Create(model);

                if (team.Id == 0)
                {
                    return BadRequest(new { message = "Something wrong happens" });
                }
                return Ok(await repo.Teamlist());
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        [ HttpPut("EditTeam/{id}")]
        public async Task<IActionResult> EditTeam(int id, TeamDto model)
        {
            if (model == null || model.Name.IsNullOrEmpty())
            {
                return BadRequest(new { message = "No Data has been Sent" });
            }
            if (await checknameWithId(id,model.Name))
            {
                return BadRequest(new { message = "team Name Already In Use" });
            }
            try
            {
                var team = await repo.Edit(id, model);

                if (team.Id == 0)
                {
                    return BadRequest(new { message = "Data Not Found" });
                }
                return Ok(await repo.Teamlist());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {

            try
            {
                var team = await repo.Delete(id);

                if (team.Id == 0)
                {
                    return BadRequest(new { message = "Data Not Found" });
                }
                return Ok(await repo.Teamlist());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        private async Task<bool>  checkname(string name)
        {
           return await repo.checkName(name) ;
        }
        private async Task<bool>  checknameWithId(int id,string name)
        {
           return await repo.checkNameWithId(id,name) ;
        }

    }
}
