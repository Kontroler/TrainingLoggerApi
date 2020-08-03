using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class Exercise: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}