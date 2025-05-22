
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentAnswers.Commands
{
    public class AddStudentAnswerCommand : IRequest<bool>
    {
        public StudentAnswerDTO StudentAnswerDTO { get; }
        public AddStudentAnswerCommand(StudentAnswerDTO studentAnswerDTO)
        {
            StudentAnswerDTO = studentAnswerDTO;
        }
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
                StudentAnswer studentAnswer = request.StudentAnswerDTO.Map<StudentAnswer>();
                generalRepository.Add(studentAnswer);
                generalRepository.Save();
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
