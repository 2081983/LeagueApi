using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueApi.Model
{
    public class Team
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Field is Required")]
        [MinLength(3, ErrorMessage = "Name Field is Required")]
        public string Name { get; set; } = "";
        public int LgId { get; set; }
        [ForeignKey(nameof(LgId))]
        public League? League { get; set; }
    }
}
