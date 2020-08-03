using System;
using System.Collections.Generic;

namespace TrainingLogger.Dtos
{
    public class TrainingForAddDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TrainingExerciseDto> Exercises { get; set; }
    }
}