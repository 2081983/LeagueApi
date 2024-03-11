namespace LeagueApi.Dto
{
    public class ResultDto
    {
        public int Id { get; set; } 
        public int MatchId { get; set; } 

        public int fteam { get; set; }
        public int seceam { get; set; }
        public int FScore { get; set; }
        public int SecScore { get; set; }
        public int FType { get; set; }
        public int Secype { get; set; }
    }
}
