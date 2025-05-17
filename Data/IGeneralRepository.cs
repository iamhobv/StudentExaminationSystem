using System.Linq.Expressions;
using StudentExamSystem.CQRS.Exams.Queries;
using StudentExamSystem.DTOs.QuestionDTOs;
using StudentExamSystem.Models;

namespace StudentExamSystem.Data
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        void Add(T Item);
        void AddList(List<T> Items);
        void Update(T Item);
        void Delete(T Item);
        void Remove(int Id);
        void Save();
        T GetByID(int Id);
        IQueryable<T> GetAll();
        IQueryable<T> GetFilter(Expression<Func<T, bool>> expression);
    }
}
