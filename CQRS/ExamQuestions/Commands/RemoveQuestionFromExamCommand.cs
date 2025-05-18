
namespace StudentExamSystem.CQRS.ExamQuestions.Commands
{
    public class RemoveQuestionFromExamCommand : IRequest<bool>
    {
        public int ExamId { get; }
        public int QuestionId { get; }

        public RemoveQuestionFromExamCommand(int examId, int questionId)
        {
            ExamId = examId;
            QuestionId = questionId;
        }
    }
    public class RemoveQuestionFromExamCommandHandler : IRequestHandler<RemoveQuestionFromExamCommand, bool>
    {

        private readonly IGeneralRepository<ExamQuestion> repository;

        public RemoveQuestionFromExamCommandHandler(IGeneralRepository<ExamQuestion> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(RemoveQuestionFromExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var examQuestion = repository.GetAll()
                .FirstOrDefault(eq => eq.ExamID == request.ExamId && eq.QuestionID == request.QuestionId);

                if (examQuestion == null)
                    return Task.FromResult(false);

                repository.Delete(examQuestion);
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
