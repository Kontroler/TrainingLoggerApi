using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.API.Dtos
{
    public class UserForLoginDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }  
    }
}