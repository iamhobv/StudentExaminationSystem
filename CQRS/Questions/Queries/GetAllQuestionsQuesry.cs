
namespace StudentExamSystem.CQRS.Questions.Queries
{
    public class GetAllQuestionsQuesry : IRequest<IEnumerable<GetQuestionDTO>>
    {
    }

    public class GetAllQuestionsHandler : IRequestHandler<GetAllQuestionsQuesry, IEnumerable<GetQuestionDTO>>
    {

        private readonly IGeneralRepository<Question> repository;
        private readonly IMediator mediator;

        public GetAllQuestionsHandler(IGeneralRepository<Question> repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<IEnumerable<GetQuestionDTO>> Handle(GetAllQuestionsQuesry request, CancellationToken cancellationToken)
        {
            var questions = repository.GetFilter(q => q.IsDeleted == false).ProjectTo<GetQuestionDTO>().ToList();

            if (questions.Any())
            {
                foreach (var q in questions)
                {
                    if (q.QuestionType == QuestionTypes.MCQ)
                    {
                        var res = await mediator.Send(new GetOptionsByQuestionIdQuery() { QuestionId = q.QuestionId });
                        q.Options = res.ToList();
                    }
                }
            }
            return questions;
        }
    }
}
