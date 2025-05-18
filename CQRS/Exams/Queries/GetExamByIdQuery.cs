using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.QuestionDTOs;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class GetExamByIdQuery : IRequest<List<TakeExamDTO>>
    {
        public int Id { get; set; }
        public GetExamByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetExamByIdHandler : IRequestHandler<GetExamByIdQuery,List<TakeExamDTO>>
    {
        private readonly IGeneralRepository<Exam> repository;
        public GetExamByIdHandler(DataBaseContext context,IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }

        public async Task<List<TakeExamDTO>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var exam = await repository.GetAll()
              .Include(e => e.ExamQuestions)
              .ThenInclude(eq => eq.Question)
             .FirstOrDefaultAsync(e => e.ID == request.Id);

            if (exam == null)
            {
                return new List<TakeExamDTO>();
            }

            var result = exam.ExamQuestions.Select(eq => new TakeExamDTO
            {
                QuestionId = eq.Question.ID,
                QuestionBody = eq.Question.QuestionBody,
                QuestionType = eq.Question.QuestionType,
                QuestionMark = eq.Question.QuestionMark,
                ExamTitle = exam.Title
            }).ToList();

            return result;

        }
    }
}
