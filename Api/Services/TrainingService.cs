using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrainingLogger.API.Data;
using TrainingLogger.Data;
using TrainingLogger.Dtos;
using TrainingLogger.Exceptions;
using TrainingLogger.Models;

namespace TrainingLogger.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _repoTraining;
        private readonly IUserRepository _repoUser;
        private readonly IExerciseRepository _repoExercise;
        private readonly IUnitRepository _repoUnit;
        private readonly IMapper _mapper;
        public TrainingService(
            ITrainingRepository repoTraining,
            IUserRepository repoUser,
            IExerciseRepository repoExercise,
            IUnitRepository repoUnit,
            IMapper mapper
            )
        {
            _repoTraining = repoTraining;
            _repoUser = repoUser;
            _repoExercise = repoExercise;
            _repoUnit = repoUnit;
            _mapper = mapper;
        }

        public async Task<bool> Add(TrainingForAddDto trainingForAddDto, int userId)
        {
            var user = await _repoUser.GetUser(userId);

            var trainingToCreate = Training.Create(trainingForAddDto.Name.Trim(), trainingForAddDto.Date, user);

            foreach (var trainingExerciseDto in trainingForAddDto.Exercises)
            {
                var exercise = await _repoExercise.GetByName(trainingExerciseDto.Exercise.Name, user.Id);
                if (exercise == null)
                {
                    _repoExercise.Add(Exercise.Create(trainingExerciseDto.Exercise.Name, user));
                    await _repoExercise.SaveAll();
                    exercise = await _repoExercise.GetByName(trainingExerciseDto.Exercise.Name, user.Id);
                }
                var exerciseToCreate = TrainingExercise.Create(exercise, user);

                foreach (var set in trainingExerciseDto.Sets)
                {
                    var unit = await _repoUnit.GetByCode(set.Unit);
                    var setToCreate = TrainingExerciseSet.Create(set.Reps, set.Weight, unit, user);
                    exerciseToCreate.Sets.Add(setToCreate);
                }

                trainingToCreate.Exercises.Add(exerciseToCreate);
            }
            _repoTraining.Add(trainingToCreate);
            return await _repoTraining.SaveAll();
        }

        public async Task<bool> Delete(int trainingId, int userId)
        {
            var training = await _repoTraining.GetById(trainingId, userId);
            if (training == null)
            {
                throw new MissingEntityException($"Training does not exists in database. Training id: ${trainingId}");
            }

            _repoTraining.Delete(training);
            return await _repoTraining.SaveAll();
        }

        public async Task<IEnumerable<TrainingDto>> GetAllByUserId(int userId)
        {
            var trainings = await _repoTraining.GetAllByUserId(userId);

            var trainingDtos = _mapper.Map<IEnumerable<TrainingDto>>(trainings);
            return trainingDtos;
        }

        public async Task<IEnumerable<TrainingNameResponseDto>> GetAllTrainingNames(int userId)
        {
            var trainingNames = await _repoTraining.GetAllTrainingNames(userId);

            var trainingNamesResponseDtos = trainingNames.Select(name => new TrainingNameResponseDto { Name = name });
            return trainingNamesResponseDtos;
        }

        public async Task<TrainingDto> GetById(int trainingId, int userId)
        {
            var training = await _repoTraining.GetById(trainingId, userId);
            var trainingDto = _mapper.Map<TrainingDto>(training);
            return trainingDto;
        }

        public Task<bool> Update(TrainingForUpdateRequestDto trainingForUpdateRequestDto, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}