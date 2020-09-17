using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingLogger.Dtos
{
    public class TrainingForUpdateRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ICollection<TrainingExerciseDto> Exercises { get; set; }
    }
}
