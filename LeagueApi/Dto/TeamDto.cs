using System.ComponentModel.DataAnnotations;

namespace LeagueApi.Dto
{
    public class TeamDto
    {
        [Required(ErrorMessage = "Name Field is Required")]
        [MinLength(3, ErrorMessage = "Name Field is Required")]
        public string Name { get; set; } = "";

 
    }
}
