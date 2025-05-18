
using StudentExamSystem.CQRS.ExamQuestions.Commands;
using StudentExamSystem.CQRS.Exams.Commands;

namespace StudentExamSystem.CQRS.Exams.Orchesterator
{
    public class UpdateExamAddQuestionOrchesterator:IRequest<bool>
    {
        
        public UpdateExamAddQuestionOrchesterator(updateExamDTO examDTO)
        {
            ExamDTO = examDTO;
        }
        public updateExamDTO ExamDTO { get; }
    }
    public class UpdateExamAddQuestionOrchesteratorHandler : IRequestHandler<UpdateExamAddQuestionOrchesterator, bool>
    {
        private readonly DataBaseContext context;
        private readonly Mediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public UpdateExamAddQuestionOrchesteratorHandler(IGeneralRepository<Exam> repository , DataBaseContext context ,Mediator mediator, IHttpContextAccessor httpContextAccessor , UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(UpdateExamAddQuestionOrchesterator request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var user = userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return false;
            }

            foreach (var q in request.ExamDTO.QuestionID)
            {
               await mediator.Send(new AddQuestionToExamCommand(request.ExamDTO.ExamId, q), cancellationToken);
            }


            var addQuestion = await mediator.Send(new UpdateExamCommand(request.ExamDTO), cancellationToken);

            return addQuestion.IsSuccess;
        }
    }
}
