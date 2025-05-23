
using MediatR;
using StudentExamSystem.CQRS.MCQOtions.Queries;

namespace StudentExamSystem.CQRS.Questions.Queries
{
    public class GetExamQuestionsQuery : IRequest<GetQuestionDTO>
    {
        public int ID { get; set; }
    }

    public class GetExamQuestionsQueryHandler : IRequestHandler<GetExamQuestionsQuery, GetQuestionDTO>
    {
        private readonly IGeneralRepository<Question> repository;
        private readonly IMediator mediator;

        public GetExamQuestionsQueryHandler(IGeneralRepository<Question> repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }
        public async Task<GetQuestionDTO> Handle(GetExamQuestionsQuery request, CancellationToken cancellationToken)
        {
            var question = repository.GetFilter(q => q.IsDeleted == false && q.ID == request.ID).ProjectTo<GetQuestionDTO>().FirstOrDefault();

            if (question != null)
            {

                if (question.QuestionType == QuestionTypes.MCQ)
                {
                    var res = await mediator.Send(new GetOptionsByQuestionIdQuery() { QuestionId = question.QuestionId });
                    question.Options = res.ToList();
                }
            }

            return question;
        }
    }
}
