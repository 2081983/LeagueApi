using LeagueApi.Dto;
using LeagueApi.Model;

namespace LeagueApi.Dependency.IRepository
{
    public interface IGroupRepository
    {
         Task<List<Group>> Grouplist();
         Task<Group> GetGroup(int id);
         Task<Group> Create(GroupDto  model);
         Task<Group> Edit(int id, GroupDto model);
         Task<Group> Delete(int id);
        Task<bool> checkName(string name);
        Task<bool> checkNameWithId(int id, string name);
    }
}
