using System.Linq.Expressions;
using StudentExamSystem.Models;

namespace StudentExamSystem.Data
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseModel
    {
        private readonly DataBaseContext context;

        public GeneralRepository(DataBaseContext context)
        {
            this.context = context;
        }
        public void Add(T Item)
        {
            context.Set<T>().Add(Item);
        }
        public void AddList(List<T> Items)
        {
            context.Set<T>().AddRange(Items);
        }
        public IQueryable<T> GetFilter(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetByID(int Id)
        {
            return GetFilter(x => x.ID == Id).FirstOrDefault();
        }

        public void Remove(int Id)
        {
            T entity = GetByID(Id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }

        public void Update(T Item)
        {
            context.Set<T>().Update(Item);
        }
        public void Delete(T Item)
        {
            context.Set<T>().Remove(Item);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
