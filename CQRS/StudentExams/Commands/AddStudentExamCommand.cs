
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentExams.Commands
{
    public class AddStudentExamCommand : IRequest<bool>
    {
        public studentExamDTO StudentExamDTO { get; }
        public AddStudentExamCommand(studentExamDTO studentExamDTO)
        {
            this.StudentExamDTO = studentExamDTO;
        }
    }
    public class AddStudentExamHandler : IRequestHandler<AddStudentExamCommand, bool>
    {
        private readonly IGeneralRepository<StudentExam> generalRepository;
        public AddStudentExamHandler(IGeneralRepository<StudentExam> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public Task<bool> Handle(AddStudentExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StudentExam studentExam = request.StudentExamDTO.Map<StudentExam>();
                generalRepository.Add(studentExam);
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
