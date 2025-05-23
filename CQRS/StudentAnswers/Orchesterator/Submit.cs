
using StudentExamSystem.CQRS.StudentAnswers.Commands;
using StudentExamSystem.CQRS.StudentExams.Commands;
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.CQRS.StudentAnswers.Orchesterator
{
    public class Submit : IRequest<bool>
    {
        public string StudentID { get; set; }

        public int ExamID { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<StudentAnswerDTO> StudentAnswerDTOList { get; set; }


        public class SubmitHandler : IRequestHandler<Submit, bool>
        {
            private readonly IMediator mediator;


            public SubmitHandler(IMediator mediator)
            {
                this.mediator = mediator;
            }
            public async Task<bool> Handle(Submit request, CancellationToken cancellationToken)
            {
                try
                {
                    studentExamDTO studentExamDTO = new studentExamDTO { ExamID = request.ExamID, StartedAt = request.StartedAt, StudentID = request.StudentID, SubmittedAt = request.SubmittedAt };
                    var AddStudentExamCommandRes = await mediator.Send(new AddStudentExamCommand(studentExamDTO));


                    bool AddStudentAnswerCommandRes = false;
                    foreach (StudentAnswerDTO studentAnswerDTO in request.StudentAnswerDTOList)
                    {
                        studentAnswerDTO.StudentID = request.StudentID;
                        studentAnswerDTO.ExamID = request.ExamID;

                        AddStudentAnswerCommandRes = await mediator.Send(new AddStudentAnswerCommand() { StudentAnswerDTO = studentAnswerDTO });
                    }

                    if (AddStudentExamCommandRes && AddStudentAnswerCommandRes)
                    {

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
    }
}
