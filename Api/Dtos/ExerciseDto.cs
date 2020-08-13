using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.Dtos
{
    public class ExerciseDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}