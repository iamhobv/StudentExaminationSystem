using MediatR;
using StudentExamSystem.Data;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Questions.Queries
{
    public class GetQuestionByIdQuery : IRequest<Question>
    {
        public int Id { get; set; }
    }
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Question>
    {
        private readonly IGeneralRepository<Question> repository;

        public GetQuestionByIdQueryHandler(IGeneralRepository<Question> repository)
        {
            this.repository = repository;
        }
        public Task<Question> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(repository.GetFilter(q => q.IsDeleted == false && q.ID == request.Id).FirstOrDefault());
        }
    }
}
