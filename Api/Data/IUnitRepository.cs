using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public interface IUnitRepository: IBaseRepository<Unit>
    {
         Task<Unit> GetByCode(string code);
         Task<IEnumerable<Unit>> GetAll();
    }
}