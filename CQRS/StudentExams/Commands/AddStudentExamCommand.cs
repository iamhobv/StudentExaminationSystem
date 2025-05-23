
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
                StudentExam studentExam = new StudentExam()
                {
                    CreatedAt = DateTime.Now,
                    ExamID = request.StudentExamDTO.ExamID,
                    IsDeleted = false,
                    StartedAt = request.StudentExamDTO.StartedAt,
                    StudentID = request.StudentExamDTO.StudentID,
                    SubmittedAt = request.StudentExamDTO.SubmittedAt
                };
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
