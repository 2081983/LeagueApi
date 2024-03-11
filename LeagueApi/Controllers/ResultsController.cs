using LeagueApi.Dependency.IRepository;
using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IMatchRepository repo;

        public ResultsController(IMatchRepository _repo)
        {
            repo = _repo;
        }


        [HttpGet("GetMatchsList/{id?}")]
        public async Task<IActionResult> GetMatchsList(int id=1) {

            List<Match> matchs =await repo.getList(id);

            List<MatchDto>dtos = new List<MatchDto>();
            foreach (Match match in matchs)
            {
                var dif = DateTime.Now - match.Date;
                
                MatchDto dto = new MatchDto()
                {
                
                    Id=match.Id,
                    FTeamId =match.TeamId1,
                    SecTeamId=match.TeamId2,
                    FteamName=match.fteam.Name,
                    SecTeamName=match.steam.Name,
                    GroupId=match.GroupId,
                    groupName=match.group.Name,
                    state=match.state,
                    MatchDate=match.Date,
                    ended=dif.Hours>=3
                };
                dto.MatchId = match.Id; 
                dtos.Add(dto);
            }

            if (matchs==null)
            {
                return BadRequest("there's no Matchs In The League");
            }
            return Ok(dtos);
        
        }


        [HttpPost("addmatchres")]
        public async Task<IActionResult> addmatchres(ResultDto dto)
         {
            if (dto==null)
            {
                return BadRequest(new { message = "Data Not Valid" });
            }
          await  repo.addResult(dto);
            return Ok(dto);

        } 

    }


   
   
}
