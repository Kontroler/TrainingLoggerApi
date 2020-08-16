using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.Dtos
{
    public class TrainingDto
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ICollection<TrainingExerciseDto> Exercises { get; set; }
    }
}