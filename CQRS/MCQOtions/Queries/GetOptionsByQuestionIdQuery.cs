using MediatR;
using StudentExamSystem.Data;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.MCQOtions.Queries
{
    public class GetOptionsByQuestionIdQuery : IRequest<IEnumerable<string>>
    {
        public int QuestionId { get; set; }
    }
    public class GetOptionsByQuestionIdQueryHandler : IRequestHandler<GetOptionsByQuestionIdQuery, IEnumerable<string>>
    {
        private readonly IGeneralRepository<MCQAnswerOptions> repository;

        public GetOptionsByQuestionIdQueryHandler(IGeneralRepository<MCQAnswerOptions> repository)
        {
            this.repository = repository;
        }
        public Task<IEnumerable<string>> Handle(GetOptionsByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var option = repository.GetFilter(o => o.IsDeleted == false && o.QuestionID == request.QuestionId).Select(o => o.Option).AsEnumerable();
            return Task.FromResult(option);
        }
    }
}
