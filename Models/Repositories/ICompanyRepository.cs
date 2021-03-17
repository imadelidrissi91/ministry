using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Models.Repositories
{
    public interface ICompanyRepository<TEntity>
    {
        TEntity Get(int id);
        //List view
        IEnumerable<TEntity> GetEntities();
        void Add(TEntity entity);
        TEntity Update(TEntity entityChanges);
        TEntity Delete(int id);


    }
}
