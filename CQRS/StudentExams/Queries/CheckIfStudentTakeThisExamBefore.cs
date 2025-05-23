
namespace StudentExamSystem.CQRS.StudentExams.Queries
{
    public class CheckIfStudentTakeThisExamBefore : IRequest<bool>
    {
        public string StdId { get; set; }
        public int ExamID { get; set; }
    }
    public class CheckIfStudentTakeThisExamBeforeHandler : IRequestHandler<CheckIfStudentTakeThisExamBefore, bool>
    {
        private readonly IGeneralRepository<StudentExam> repository;

        public CheckIfStudentTakeThisExamBeforeHandler(IGeneralRepository<StudentExam> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(CheckIfStudentTakeThisExamBefore request, CancellationToken cancellationToken)
        {
            var IfStudentTakeThisExam = repository.GetFilter(e => e.IsDeleted == false && e.StudentID.Equals(request.StdId) && e.ExamID == request.ExamID).FirstOrDefault();
            if (IfStudentTakeThisExam == null)
            {
                //student does not take the exam
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
