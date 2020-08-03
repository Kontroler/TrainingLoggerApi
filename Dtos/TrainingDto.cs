using System;
using System.Collections;
using System.Collections.Generic;

namespace TrainingLogger.Dtos
{
    public class TrainingDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TrainingExerciseDto> Exercises { get; set; }
    }
}