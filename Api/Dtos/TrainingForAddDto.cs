using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.Dtos
{
    public class TrainingForAddDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ICollection<TrainingExerciseDto> Exercises { get; set; }
    }
}