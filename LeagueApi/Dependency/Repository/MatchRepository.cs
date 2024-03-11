using LeagueApi.Dependency.IRepository;
using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.EntityFrameworkCore;


namespace LeagueApi.Dependency.Repository
{
    public class MatchRepository : IMatchRepository
    {
        public MatchRepository(AppDb _db)
        {
            Db = _db;
        }

        public AppDb Db { get; }

        public async Task<ResultDto> addResult(ResultDto dto)
        {
          
            int score = (dto.FScore - dto.SecScore);
            int fpoints,scpoints;
            byte frestype,secrestype;
            if (score>0)
            {
                fpoints = 3; scpoints = 0;
                frestype=1;secrestype=2;

            }else if (score<0)
            {
                fpoints = 0; scpoints = 3;
                frestype = 2; secrestype = 1;

            }
            else
            {
                fpoints = 1; scpoints = 1;
                frestype = 3; secrestype = 3;
            }
            var matchid = Db.Matches.First(x => x.TeamId1 == dto.fteam && x.TeamId2 == dto.seceam).Id;
            Result rs = new Result
            {
                MatchId = matchid,
                Score=dto.FScore,
                Receive=dto.SecScore,
                Point=fpoints,
                TeamId=dto.fteam,
                ResultType=frestype
            };
            Result rs2 = new Result
            {
                MatchId = dto.Id,
                Score = dto.SecScore,
                Receive = dto.FScore,
                Point=scpoints,
                TeamId = dto.seceam,
                ResultType = secrestype
            };
            await Db.Results.AddAsync(rs);     
            await Db.Results.AddAsync(rs2);
           var match= await Db.Matches.FindAsync(dto.Id);
            match.state = 2;
            Db.Matches.Update(match);
           await Db.SaveChangesAsync();
            return (dto);
        }

        public async Task<List<Match>> getList(int lgid)
        {
         List<Match>matchs= await  Db.Matches.Where(x=>x.group.LgId==lgid) .ToListAsync();
          var match=await  Db.Matches.Where(x => x.group.LgId == lgid).Include(x => x.fteam).Include(x => x.steam).Include(x => x.group).ToListAsync();
              return matchs;
        }
    }
}
