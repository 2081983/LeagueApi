namespace LeagueApi.Dto
{
    public class MatchDto
    {

        public int Id { get; set; }
        public int MatchId { get; set; }
        public int GroupId { get; set; }
        public string groupName { get; set; } = "";
        public int FTeamId { get; set; }
        public string FteamName { get; set; } = "";
        public int SecTeamId { get; set; }
        public string SecTeamName { get; set; } = "";
        public DateTime MatchDate { get; set; }
        public byte state { get; set; }
        public bool? ended { get; set; }
 

    }
}