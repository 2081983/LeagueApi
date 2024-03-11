using LeagueApi.Dependency.IRepository;
using LeagueApi.Dto;
using LeagueApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace LeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository repo;

        public GroupsController(IGroupRepository _repo)
        {
            repo = _repo;
        }

         [HttpGet("GetAll")]
         public async Task<IActionResult> GetAll ()
         {
            try
            {
                var groups = await repo.Grouplist();

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }


        [HttpGet("GetGroup")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await repo.GetGroup(id);
                if (group.Id == 0)
                {
                    return BadRequest(new { message = "no data has been found" });
                }
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGroup")]
           public async Task<IActionResult> AddGroup(GroupDto model)
           {
            if (model==null || model.Name.IsNullOrEmpty())
            {
                return BadRequest(new {message="No Data has been Sent"});
            }
            if (await checkname(model.Name))
            {
                return BadRequest(new { message = "team Name Already In Use" });
            }
            try
            {
                var group = await repo.Create(model);

                if (group.Id == 0)
                {
                    return BadRequest(new { message = "Something wrong happens" });
                }
                return Ok(await repo.Grouplist());
 
            }
            catch (Exception ex)
            {

                return BadRequest(new {message= ex.Message });
            }
 
           }

       [HttpPut("EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(int id,GroupDto model)
        {
            if (model == null || model.Name.IsNullOrEmpty())
            {
                return BadRequest(new { message = "No Data has been Sent" });
            }
            if (await checknameWithId(id, model.Name))
            {
                return BadRequest(new { message = "team Name Already In Use" });
            }

            try
            {
                var group = await repo.Edit(id, model);
              
                if (group.Id==0)
                {
                    return BadRequest(new { message = "Data Not Found"});
                }
                return Ok(await repo.Grouplist());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpDelete("DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
  
            try
            {
                var group = await repo.Delete(id); 

                if (group.Id == 0)
                {
                    return BadRequest(new { message = "Data Not Found" });
                }
                return Ok(await repo.Grouplist()); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        private async Task<bool> checkname(string name)
        {
            return await repo.checkName(name);
        }
        private async Task<bool> checknameWithId(int id, string name)
        {
            return await repo.checkNameWithId(id, name);
        }



    }
}
