
using StudentExamSystem.CQRS.StudentAnswers.Commands;
using StudentExamSystem.CQRS.StudentExams.Commands;
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentAnswers.Orchesterator
{
    public class Submit : IRequest<bool>
    {
        public List<StudentAnswerDTO> studentAnswerDTO { get; }
        public List<studentExamDTO> studentExamDTO { get; }
        public Submit(List<StudentAnswerDTO> studentAnswerDTO,List<studentExamDTO> studentExamDTO)
        {
            this.studentAnswerDTO = studentAnswerDTO;
            this.studentExamDTO = studentExamDTO;
        }

        public class SubmitHandler : IRequestHandler<Submit,bool>
        {
            private readonly IMediator mediator;
            public Task<bool> Handle(Submit request, CancellationToken cancellationToken)
            {
                try
                {
                    foreach (StudentAnswerDTO studentAnswerDTO in request.studentAnswerDTO)
                    {
                        mediator.Send(new AddStudentAnswerCommand(studentAnswerDTO));
                    }
                    foreach(studentExamDTO studentExamDTO in request.studentExamDTO)
                    {
                        mediator.Send(new AddStudentExamCommand(studentExamDTO));
                    }
                    return Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    return Task.FromResult(false);
                }

            }
        }
    }
}
