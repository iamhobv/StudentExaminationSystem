using System.Linq.Expressions;
using StudentExamSystem.Models;

namespace StudentExamSystem.Data
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        void Add(T Item);
        void Update(T Item);
        void Delete(T Item);
        void Remove(int Id);
        void Save();
        T GetByID(int Id);
        IQueryable<T> GetAll();
        IQueryable<T> GetFilter(Expression<Func<T, bool>> expression);
    }
}
