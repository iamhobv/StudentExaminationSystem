//namespace StudentExamSystem.CQRS.Exams.Commands
//{
//    public class UpdateExamQuestionsCommand:IRequest<ResponseDTO<string>>
//    {
//        public List<ExamQuestionDTO> Questions { get; set; }

//        public UpdateExamQuestionsCommand(int examID, List<ExamQuestionDTO> questions)
//        {
//            Questions = questions;
//        }
//    }


//    public class UpdateExamQuestionsCommandHandler : IRequestHandler<UpdateExamQuestionsCommand, ResponseDTO<string>>
//    {
//        private readonly IGeneralRepository<ExamQuestion> repository;

//        public UpdateExamQuestionsCommandHandler(IGeneralRepository<ExamQuestion> repository)
//        {
//            this.repository = repository;
//        }
//        public Task<ResponseDTO<string>> Handle(UpdateExamQuestionsCommand request, CancellationToken cancellationToken)
//        {
            
//        }
//    }
//}
