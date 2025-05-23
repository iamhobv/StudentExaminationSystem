
using StudentExamSystem.CQRS.StudentAnswers.Commands;
using StudentExamSystem.CQRS.StudentExams.Commands;
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentAnswers.Orchesterator
{
    public class Submit : IRequest<bool>
    {
        public List<StudentAnswerDTO> studentAnswerDTOList { get; }
        public List<studentExamDTO> studentExamDTOList { get; }
        public Submit(List<StudentAnswerDTO> studentAnswerDTOList,List<studentExamDTO> studentExamDTOList)
        {
            this.studentAnswerDTOList = studentAnswerDTOList;
            this.studentExamDTOList = studentExamDTOList;
        }

        public class SubmitHandler : IRequestHandler<Submit,bool>
        {
            private readonly IMediator mediator;
            public Task<bool> Handle(Submit request, CancellationToken cancellationToken)
            {
                try
                {
                    foreach (StudentAnswerDTO studentAnswerDTO in request.studentAnswerDTOList)
                    {
                        mediator.Send(new AddStudentAnswerCommand(studentAnswerDTO));
                    }
                    foreach(studentExamDTO studentExamDTO in request.studentExamDTOList)
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
