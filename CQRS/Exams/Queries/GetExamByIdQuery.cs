using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.Student;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class GetExamByIdQuery : IRequest<TakeExamDTO>
    {
        public int Id { get; set; }
        public GetExamByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetExamByIdHandler : IRequestHandler<GetExamByIdQuery, TakeExamDTO>
    {
        private readonly IGeneralRepository<Exam> repository;
        private readonly IMediator mediator;

        public GetExamByIdHandler(DataBaseContext context, IGeneralRepository<Exam> repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<TakeExamDTO> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            Exam exam = await repository.GetAll().Where(e => e.IsDeleted == false)
              .Include(e => e.ExamQuestions)
              .ThenInclude(eq => eq.Question).ThenInclude(q => q.MCQAnswerOptions)
             .FirstOrDefaultAsync(e => e.ID == request.Id);
            if (exam == null)
            {
                return null;
            }
            List<GetQuestionDTO> getQuestions = new List<GetQuestionDTO>();
            foreach (var item in exam.ExamQuestions)
            {
                var res = await mediator.Send(new GetExamQuestionsQuery() { ID = item.QuestionID });
                if (res != null)
                {
                    getQuestions.Add(res);
                }

            }

            var result = new TakeExamDTO()
            {
                ExamTitle = exam.Title,

                Questions = getQuestions,
                Duration = exam.Duration
            };
            //    .select(eq => new TakeExamDTO
            //{
            //    ExamTitle = exam.Title,
            //    Questions = getQuestions,
            //});


            return result;

        }
    }
}
