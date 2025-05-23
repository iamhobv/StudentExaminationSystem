
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentAnswers.Commands
{
    public class AddStudentAnswerCommand : IRequest<bool>
    {
        //public StudentAnswerDTO StudentAnswerDTO { get; }
        public StudentAnswerDTO StudentAnswerDTO { get; set; }
        //public AddStudentAnswerCommand(StudentAnswerDTO studentAnswerDTO)
        //{
        //    StudentAnswerDTO = studentAnswerDTO;
        //}
    }

    public class AddStudentAnswerHandler : IRequestHandler<AddStudentAnswerCommand, bool>
    {
        private readonly IGeneralRepository<StudentAnswer> generalRepository;
        public AddStudentAnswerHandler(IGeneralRepository<StudentAnswer> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public Task<bool> Handle(AddStudentAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StudentAnswer studentAnswer = new StudentAnswer()
                {
                    CreatedAt = DateTime.UtcNow,
                    ExamID = (int)request.StudentAnswerDTO.ExamID,
                    IsDeleted = false,
                    QuestionID = request.StudentAnswerDTO.QuestionID,

                    StudentID = request.StudentAnswerDTO.StudentID,
                    StudentQuestionAnswer = request.StudentAnswerDTO.StudentQuestionAnswer



                };
                generalRepository.Add(studentAnswer);
                generalRepository.Save();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
