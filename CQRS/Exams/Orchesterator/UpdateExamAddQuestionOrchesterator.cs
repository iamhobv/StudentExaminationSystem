
using Microsoft.EntityFrameworkCore;
using StudentExamSystem.CQRS.ExamQuestions.Commands;
using StudentExamSystem.CQRS.Exams.Commands;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Orchesterator
{
    public class UpdateExamAddQuestionOrchesterator : IRequest<bool>
    {
        public UpdateExamAddQuestionOrchesterator(updateExamDTO examDTO)
        {
            ExamDTO = examDTO;
        }
        public updateExamDTO ExamDTO { get; }
    }
    public class UpdateExamAddQuestionOrchesteratorHandler : IRequestHandler<UpdateExamAddQuestionOrchesterator, bool>
    {
        private readonly IGeneralRepository<Exam> repository;
        private readonly IGeneralRepository<ExamQuestion> examQuestionRepository;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public UpdateExamAddQuestionOrchesteratorHandler(IGeneralRepository<Exam> repository, IGeneralRepository<ExamQuestion> examQuestionRepository, IMediator mediator, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.examQuestionRepository = examQuestionRepository;
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(UpdateExamAddQuestionOrchesterator request, CancellationToken cancellationToken)
        {

            var currentUser = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            if (currentUser == null)
                return false;


            if (!await userManager.IsInRoleAsync(currentUser, "Teacher"))
                return false;

            var exam =  repository.GetByID(request.ExamDTO.ExamId);
            if (exam == null)
                return false;

            if (exam.TeacherId != currentUser.Id)
                return false;




            var existingQuestionIds =await examQuestionRepository
           .GetFilter(eq => eq.ExamID == request.ExamDTO.ExamId)
           .Select(eq => eq.QuestionID)
           .ToListAsync(cancellationToken);


            var newQuestionIds = request.ExamDTO.QuestionID ?? new List<int>();

         

            var existingSet = new HashSet<int>(existingQuestionIds);
            var newSet = new HashSet<int>(newQuestionIds);

            var toRemove = existingQuestionIds.Except(newSet).ToList();

            var toAdd = newQuestionIds.Except(existingSet).ToList();


            foreach (var questionId in toRemove)
            {
                await mediator.Send(new RemoveQuestionFromExamCommand(request.ExamDTO.ExamId, questionId), cancellationToken);
            }

            foreach (var questionId in toAdd)
            {
                await mediator.Send(new AddQuestionToExamCommand(request.ExamDTO.ExamId, questionId), cancellationToken);
            }

            var updateResult = await mediator.Send(new UpdateExamCommand(request.ExamDTO.ExamId,request.ExamDTO), cancellationToken);

            return updateResult.IsSuccess;


        }
    }
}





//var httpContext = httpContextAccessor.HttpContext;
//var user = userManager.GetUserAsync(httpContext.User);
//if (user == null)
//{
//    return false;
//}
//foreach (var q in request.ExamDTO.QuestionID)
//{
//    await mediator.Send(new AddQuestionToExamCommand(request.ExamDTO.ExamId, q), cancellationToken);
//}
//var addQuestion = await mediator.Send(new UpdateExamCommand(request.ExamDTO), cancellationToken);

//return addQuestion.IsSuccess;