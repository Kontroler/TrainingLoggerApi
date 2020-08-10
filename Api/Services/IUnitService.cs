using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.Dtos;

namespace Api.Services
{
    public interface IUnitService
    {
         Task<IEnumerable<UnitDto>> GetAll();
    }
}