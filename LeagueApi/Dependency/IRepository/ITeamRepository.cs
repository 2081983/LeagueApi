using LeagueApi.Dto;
using LeagueApi.Model;

namespace LeagueApi.Dependency.IRepository
{
    public interface ITeamRepository
    {
         Task<List<Team>> Teamlist();
         Task<Team> GetTeam(int id);
         Task<Team> Create(TeamDto  model);
         Task<Team> Edit(int id, TeamDto model);
         Task<Team> Delete(int id);
        Task<bool> checkName(string name);
        Task<bool> checkNameWithId(int id,string name);
    
    }
}
