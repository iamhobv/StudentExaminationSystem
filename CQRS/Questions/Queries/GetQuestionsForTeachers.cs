

namespace StudentExamSystem.CQRS.Questions.Queries
{
    public class GetQuestionsForTeachers : IRequest<IEnumerable<GetTeacherQuestionDTO>>
    {
    }
    public class GetQuestionsForTeachersHandler : IRequestHandler<GetQuestionsForTeachers, IEnumerable<GetTeacherQuestionDTO>>
    {

        private readonly IGeneralRepository<Question> repository;
        private readonly IMediator mediator;

        public GetQuestionsForTeachersHandler(IGeneralRepository<Question> repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<IEnumerable<GetTeacherQuestionDTO>> Handle(GetQuestionsForTeachers request, CancellationToken cancellationToken)
        {
            var questions = repository.GetFilter(q => q.IsDeleted == false).ProjectTo<GetTeacherQuestionDTO>().ToList();

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
