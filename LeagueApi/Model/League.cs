using System.ComponentModel.DataAnnotations;

namespace LeagueApi.Model
{
    public class League
    {
        [Key]
        public int Lid { get; set; }
        [Required(ErrorMessage = "League Name Is Required")]
        [MinLength(3, ErrorMessage = "League Name must be At Lest 3 character")]
        public string Name { get; set; } = "";
        // 0 matchs not added to database
        // 1 matchs added to database
        public byte state { get; set; } = 0;
    }
}
