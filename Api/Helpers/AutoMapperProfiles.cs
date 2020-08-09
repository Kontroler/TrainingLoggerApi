using AutoMapper;
using TrainingLogger.API.Dtos;
using TrainingLogger.API.Models;
using TrainingLogger.Dtos;
using TrainingLogger.Models;

namespace TrainingLogger.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Unit, UnitDto>();
            CreateMap<TrainingExerciseSet, TrainingExerciseSetDto>();
            CreateMap<TrainingExercise, TrainingExerciseDto>();
            CreateMap<Training, TrainingDto>();
        }
    }
}