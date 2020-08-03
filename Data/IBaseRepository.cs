using System.Threading.Tasks;

namespace TrainingLogger.API.Data
{
    public interface IBaseRepository<T> where T: class
    {
         void Add (T entity);
         void Delete (T entity);
         Task<bool> SaveAll();
    }
}