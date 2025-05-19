
namespace StudentExamSystem.CQRS.ExamQuestions.Commands
{
    public class AddQuestionToExamCommand:IRequest<bool>
    {
        public int ExamId { get; }
        public int QuestionId { get; }
        public AddQuestionToExamCommand(int ExamId, int QuestionId)
        {
            this.ExamId = ExamId;
            this.QuestionId = QuestionId;
        }  
    }
    public class AddQuestionToExamCommandHandler : IRequestHandler<AddQuestionToExamCommand, bool>
    {
        private readonly IGeneralRepository<ExamQuestion> repository;

        public AddQuestionToExamCommandHandler(IGeneralRepository<ExamQuestion> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(AddQuestionToExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
              var exists = repository.GetAll()
             .Any(eq => eq.ExamID == request.ExamId && eq.QuestionID == request.QuestionId);

                if (exists)
                    return Task.FromResult(false);


                var newExamQuestion = new ExamQuestion
                {
                    ExamID = request.ExamId,
                    QuestionID = request.QuestionId
                };

                repository.Add(newExamQuestion);
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



//public AddQuestionToExamCommand(int ExamId , int QuestionId)
//{
//    this.ExamId = ExamId;
//    this.QuestionId = QuestionId;
//}

//public int ExamId { get; }
//public int QuestionId { get; }



//    ExamQuestion newExamQuestion = new ExamQuestion()
//    {
//        ExamID = request.ExamId,
//        QuestionID = request.QuestionId

//    };
//    repository.Add(newExamQuestion);
//    repository.Save();

//    return Task.FromResult(true);

//}
//catch(Exception ex)
//{
//    return Task.FromResult(false);
//}
