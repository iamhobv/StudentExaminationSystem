
namespace StudentExamSystem.CQRS.ExamQuestions.Commands
{
    public class UpdateExamQuestionsCommand:IRequest<bool>

    {
        public int ExamId { get; }
        public List<int> NewQuestionIds { get; }

        public UpdateExamQuestionsCommand(int examId, List<int> newQuestionIds)
        {
            ExamId = examId;
            NewQuestionIds = newQuestionIds;
        }
    }

    public class UpdateExamQuestionsCommandHandler : IRequestHandler<UpdateExamQuestionsCommand, bool>
    {
        private readonly IGeneralRepository<ExamQuestion> repository;

        public UpdateExamQuestionsCommandHandler(IGeneralRepository<ExamQuestion> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(UpdateExamQuestionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ExistingExamQuestions = repository
                    .GetAll()
                    .Where(eq => eq.ExamID == request.ExamId);


                var existingQuestionIds = ExistingExamQuestions.Select(eq => eq.QuestionID).ToList();
                var newQuestionIds = request.NewQuestionIds;


                var toRemove = ExistingExamQuestions
                    .Where(eq => !newQuestionIds.Contains(eq.QuestionID))
                    .ToList();


                foreach (var item in toRemove)
                    repository.Delete(item);

                var toAdd = newQuestionIds
               .Where(qId => !existingQuestionIds.Contains(qId))
               .Select(qId => new ExamQuestion
               {
                   ExamID = request.ExamId,
                   QuestionID = qId
               })
               .ToList();

                foreach (var item in toAdd)
                    repository.Add(item);

                repository.Save();
                return Task.FromResult(true);


            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }

}
