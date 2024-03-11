using LeagueApi.Dependency.IRepository;
using LeagueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LeagueApi.Dependency.Repository
{
    public class GroupRepository:IGroupRepository

    {
        private readonly AppDb db;

        public GroupRepository(AppDb _db)
        {
            db = _db;
        }

        public async Task<Group> Create(Dto.GroupDto model)
        {
           
                var group = new Group
                {
                    Name = model.Name,
                    LgId = 1,
                    TeamsCount = model.TeamsCount
                };
                var result = await db.Groups.AddAsync(group);
                await db.SaveChangesAsync();
                return group;
        }

        public async Task<Group> Delete(int id)
        {
            try
            {
                var group = db.Groups.Find(id);
                if (group == null)
                {
                    return new Group();
                }
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
                return group;
            }
            catch
            {
                return new Group();
            }
        }

        public async Task<Group> Edit(int id, Dto.GroupDto model)
        {
            try
            {
                var group = db.Groups.Find(id);
                if (group == null )
                {
                    return new Group() ;
                }

                group.Name = model.Name;
                group.TeamsCount = model.TeamsCount;
                 db.Groups.Update(group);
                await db.SaveChangesAsync();
                return group;
            }
            catch
            {
                return new Group ();
            }
        }

        public async Task<Group> GetGroup(int id)
        {
            try
            {
                var group= await db.Groups.FindAsync(id);
                if (group==null)
                {
                    return new Group();
                }
                return group;
            }
            catch
            {
                return new Group();
            }
        }

        public async Task<List<Group>> Grouplist()
        {
         return await db.Groups.ToListAsync();
        }


        public async Task<bool> checkName(string name)
        {
            var group = await db.Groups.SingleOrDefaultAsync(x => x.Name == name);
            return group != null;
        }

        public async Task<bool> checkNameWithId(int id, string name)
        {
            var group = await db.Groups.SingleOrDefaultAsync(x => x.Name == name && x.Id != id);
            return group != null;
        }


    }
}
