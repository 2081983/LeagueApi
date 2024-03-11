using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueApi.Model
{
    public class Match
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You Must Select  Group")]

        public int GroupId { get; set; }
        [Required(ErrorMessage = "TeamId1 Field is Required")]
        public int TeamId1 { get; set; }
        [Required(ErrorMessage = "TeamId2 Field is Required")]
        public int TeamId2 { get; set; }
        [Required(ErrorMessage = "Match Date Field is Required")]
        public DateTime Date { get; set; }

        public byte state { get; set; } = 0;

        [ForeignKey(nameof(GroupId))]
        public Group? group { get; set; }
        [ForeignKey(nameof(TeamId1))]
        public Team? fteam { get; set; }
        [ForeignKey(nameof(TeamId2))]
        public Team? steam { get; set; }
    

    }
}
