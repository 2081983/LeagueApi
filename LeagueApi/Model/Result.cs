using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueApi.Model
{
    public class Result
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Match Field is Required")]
        public int MatchId { get; set; }
        [Required(ErrorMessage = "TeamId Field is Required")]
        public int TeamId { get; set; }
        [Required(ErrorMessage = "Score Field is Required")]
        public int Score { get; set; } = 0;
        public int Receive { get; set; } = 0;
        [Required(ErrorMessage = "ResultType Field is Required")]
          public int Point { get; set; }=0;
        [Required(ErrorMessage = "ResultType Field is Required")]

        //0 win - 1 - 2 loased
        public byte ResultType { get; set; }


        [ForeignKey(nameof(TeamId))]
        public Team? team { get; set; }
        [ForeignKey(nameof(MatchId))]
        public Match?   Matche { get; set; }

    }
}
