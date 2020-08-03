namespace TrainingLogger.Models
{
    public class TrainingExerciseSetRep: BaseModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal Weight { get; set; }
        public Unit Unit { get; set; }
        public TrainingExerciseSet Set { get; set; }
        public int TrainingExerciseSetId { get; set; }
    }
}