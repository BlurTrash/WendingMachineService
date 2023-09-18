using System.Linq;
using WendingMachineDAL.Entities.Base;

namespace WendingMachineDAL.RepositoryInterfaces.Base
{
    public interface IRepositoryBase<T, TId> where T : BaseEntity<TId>
    {
        IQueryable<T> GetAll();
        TId Create(T entity);
        TId Update(T entity);
        void Delete(TId entityId);
    }
}
