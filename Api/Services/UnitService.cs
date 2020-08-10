using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TrainingLogger.Data;
using TrainingLogger.Dtos;

namespace Api.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repo;
        private readonly IMapper _mapper;
        public UnitService(IUnitRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UnitDto>> GetAll()
        {
            var units = await _repo.GetAll();

            var unitDtos = _mapper.Map<IEnumerable<UnitDto>>(units);
            return unitDtos;
        }
    }
}