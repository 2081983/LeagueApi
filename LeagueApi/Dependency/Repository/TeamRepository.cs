using LeagueApi.Dependency.IRepository;
using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LeagueApi.Dependency.Repository
{
    public class TeamRepository : ITeamRepository

    {
        private readonly AppDb db;

        public TeamRepository(AppDb _db)
        {
            db = _db;
        }

        public async Task<bool> checkName(string name)
        {
            var team = await db.Teams.SingleOrDefaultAsync(x => x.Name == name);
            return team != null;
        }

        public async Task<bool> checkNameWithId(int id, string name)
        {
            var team = await db.Teams.SingleOrDefaultAsync(x => x.Name == name && x.Id!=id);
            return team != null;
        }

        public async Task<Team> Create(TeamDto model)
        {
           
                var team = new Team
                {
                    Name = model.Name,
                    LgId = 1,
                  };
                var result = await db.Teams.AddAsync(team);
                await db.SaveChangesAsync();
                return team;
        }

        public async Task<Team> Delete(int id)
        {
            try
            {
                var team = db.Teams.Find(id);
                if (team == null)
                {
                    return new Team();
                }
                db.Teams.Remove(team);
                await db.SaveChangesAsync();
                return team;
            }
            catch
            {
                return new Team();
            }
        }

        public async Task<Team> Edit(int id, TeamDto model)
        {
            try
            {
                var team = db.Teams.Find(id);
                if (team == null )
                {
                    return new Team() ;
                }

                team.Name = model.Name;
                 db.Teams.Update(team);
                await db.SaveChangesAsync();
                return team;
            }
            catch
            {
                return new Team ();
            }
        }

        public async Task<Team> GetTeam(int id)
        {
            try
            {
                var team= await db.Teams.FindAsync(id);
                if (team == null)
                {
                    return new Team();
                }
                return team;
            }
            catch
            {
                return new Team();
            }
        }

        public async Task<List<Team>> Teamlist()
        {
         return await db.Teams.ToListAsync();
        }
    }
}
