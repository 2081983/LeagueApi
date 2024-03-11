using System.ComponentModel.DataAnnotations;

namespace LeagueApi.Dto
{
    public class GroupDto
    {
        [Required(ErrorMessage = "Name Field is Required")]
        [MinLength(3, ErrorMessage = "Name Field is Required")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Team Count Field Is Required")]
        public int TeamsCount { get; set; }
 
    }
}
