using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchsController : ControllerBase
    {
        private readonly AppDb db;
        public MatchsController(AppDb _db)
        {
            db = _db;

        }

        [HttpGet("GetMatchs/{id?}")]
        public IActionResult GetMatchs(int id=1)
        {
            League lg = db.Leagues.Find(id);
            if (lg==null)
            {
                return NotFound();
            }
            
            if (lg.state>0)
            {
                return BadRequest(new { message = "League already  initialized" });
            }
            
            List<MatchDto> matches = new List<MatchDto>();
           List<Team> teams = db.Teams.Where (t=>t.LgId==id ).ToList();
            var grp = db.Groups.Where(g=>g.LgId==id).ToList();
            if (grp==null)
            {
                return BadRequest("There is No any Groups in this League");
            }
            var grpexist = db.Matches.FirstOrDefault(x => x.GroupId == grp.First().Id);
            if (grpexist!=null)
            {
                return BadRequest("League already initialized");

            }

            if (teams==null)
            {
                return BadRequest("There is No any Teams in this League");
            }

            var grteamsCount = grp.Sum(x => x.TeamsCount);
            var teamsCount = teams.Count();
            if (grteamsCount!=teamsCount)
            {
                return BadRequest("the Count Of Teams must match the count of Groups Teams");
            }
            List<Team> team1 = teams;
            foreach (var gr in grp)
            {
                // the teams of Current Group
                List<Team> teams2 = team1.Take(gr.TeamsCount).ToList();
             //remove the teams of the group from teamlist
                team1.RemoveRange(0, teams2.Count);
                myreg:
                Team tm = teams2.First();
                teams2.Remove(tm);
                foreach (var team2 in teams2)
                {
                    MatchDto match = new MatchDto()
                    {
                        Id=0,
                        GroupId = gr.Id,
                        groupName = gr.Name,
                        FTeamId =tm.Id,
                        FteamName=tm.Name,
                        SecTeamId = team2.Id ,
                        SecTeamName = team2.Name,
                        MatchDate=new DateTime(),
                       
                
                    };
                    matches.Add(match);
                    }
                if (teams2.Count > 0) {
                    goto myreg;
                }

            }
            return Ok( matches);

        }

        [HttpPost("postMatchs")]
        public IActionResult postMatchs(MatchDto[] model)
        {
  
                foreach (MatchDto item in model)
            {
                Match match = new Match
                {
                    TeamId1 = item.FTeamId,
                    TeamId2 = item.SecTeamId,
                    GroupId = item.GroupId,
                    Date = item.MatchDate,
                    state=1
                };
                db.Matches.Add(match);

            }

            db.SaveChanges();
            return Ok(model);

        }





        [HttpGet("resultreport")]
        public IActionResult resultreport()
       {

            var query = db.Results.Include(x => x.team)
                .GroupBy(m=>m.team.Name).Select(x => new ReportDto{ TeamName= x.Key, Points = x.Sum(m => m.Point), score= x.Sum(m=>m.Score), Receive = x.Sum(m => m.Receive), Goals = x.Sum(m =>m.Score - m.Receive), From = x.Count(), }).OrderByDescending(x=>x.Points).ToList();
            if (query==null)
            {
                return BadRequest(new { message = "No Data" });
            }    
                
           // query.Sum(x=>x.Score,).GroupBy(x => new { x.TeamId, x.team.Name })




         

            return Ok(query);

        }





    }
}
