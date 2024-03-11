
using LeagueApi.Dto;
using LeagueApi.Model;

namespace LeagueApi.Dependency.IRepository
{
    public interface IMatchRepository
    {
        Task<List<Match>> getList(int lgid);
        Task<ResultDto> addResult(ResultDto dto);
    }
}
