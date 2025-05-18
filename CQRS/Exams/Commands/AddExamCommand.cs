
namespace StudentExamSystem.CQRS.Exams.Commands
{
    public class AddExamCommand : IRequest<ResponseDTO<AddExamDTO>>
    {
        public AddExamDTO Exam { get; }
        public AddExamCommand(AddExamDTO exam)
        {
            Exam = exam;
        }
    }
    public class AddExamCommandHandler : IRequestHandler<AddExamCommand, ResponseDTO<AddExamDTO>>
    {
        private readonly IGeneralRepository<Exam> repository;
        private readonly IGeneralRepository<ExamQuestion> examQuestionRepo;

        public AddExamCommandHandler(IGeneralRepository<Exam> repository, IGeneralRepository<ExamQuestion> examQuestionRepo)
        {
            this.repository = repository;
            this.examQuestionRepo = examQuestionRepo;
        }

        public Task<ResponseDTO<AddExamDTO>> Handle(AddExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Exam newExam = request.Exam.Map<Exam>();
                newExam.CreatedAt = DateTime.UtcNow;
                newExam.IsDeleted = false;


                newExam.ExamQuestions = request.Exam.ExamQuestionsIDs
               .Select(qId => new ExamQuestion
               {
                   QuestionID = qId
               })
                .ToList();

                repository.Add(newExam);
                repository.Save();


                var questions = examQuestionRepo
               .GetFilter(q => request.Exam.ExamQuestionsIDs.Contains(q.QuestionID))
               .ToList();

                AddExamDTO result = newExam.Map<AddExamDTO>();

                result.ExamQuestionsIDs = newExam.ExamQuestions?.Select(eq => eq.QuestionID).ToList();

                return Task.FromResult(ResponseDTO<AddExamDTO>.Success(result, "Exam Added Successfully"));

            }
            catch(Exception ex)
            {
                return Task.FromResult(ResponseDTO<AddExamDTO>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}"));
            }
        }
    }
}
