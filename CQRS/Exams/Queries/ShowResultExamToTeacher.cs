
using Microsoft.EntityFrameworkCore;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class ShowResultExamToTeacher:IRequest<ResponseDTO<ShowResultToTeacherDTO>>
    {
        public ShowResultExamToTeacher(int examId)
        {
            ExamId = examId;
        }
        public int ExamId { get; set; }
    }

    public class ShowResultExamToTeacherHandler : IRequestHandler<ShowResultExamToTeacher, ResponseDTO<ShowResultToTeacherDTO>>
    {
        private readonly IGeneralRepository<Exam> examRepository;
        private readonly IGeneralRepository<StudentExam> studentExamRepository;
        private readonly IGeneralRepository<StudentAnswer> studentAnswerrepository;

        public ShowResultExamToTeacherHandler(IGeneralRepository<Exam> ExamRepository,IGeneralRepository<StudentExam> StudentExamRepository,IGeneralRepository<StudentAnswer> StudentAnswerrepository)
        {
            examRepository = ExamRepository;
            studentExamRepository = StudentExamRepository;
            studentAnswerrepository = StudentAnswerrepository;
        }
        public async Task<ResponseDTO<ShowResultToTeacherDTO>> Handle(ShowResultExamToTeacher request, CancellationToken cancellationToken)
        {
            var exam =await examRepository.GetFilter(e => e.ID == request.ExamId)
                .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                .FirstOrDefaultAsync();

            if (exam == null)
            {
                return ResponseDTO<ShowResultToTeacherDTO>.Error(ErrorCode.ServerError, "Exam not found");
            }

            var studentExam = await studentExamRepository.
                GetFilter(se => se.ExamID == request.ExamId)
                .Include(se => se.Student)
                .ToListAsync();

            var studentAnswers = await studentAnswerrepository
            .GetFilter(sa => sa.ExamID == request.ExamId)
            .Include(sa => sa.Question)
            .ToListAsync();
   

            var studentResults = studentExam.Select(
            se =>
            {
            var currentStudentAnswers = studentAnswers
            .Where(sa => sa.StudentID == se.StudentID)
            .ToList();

            var totalScore = currentStudentAnswers
            .Where(sa => sa.Question!=null)
             .Sum(sa =>
             {
                 var studentAnswer = sa.StudentQuestionAnswer?.Trim() ?? string.Empty;
                 var correctAnswer = sa.Question.CorrectAnswer?.Trim() ?? string.Empty;

                 return string.Equals(studentAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase)
                     ? sa.Question.QuestionMark
                     : 0;
             });


                return new StudentExamResultDto
                {
                    StudentId = se.StudentID,
                    StudentName = se.Student.UserName,
                    StartedAt = se.StartedAt,
                    SubmittedAt = se.SubmittedAt,
                    TotalScore = totalScore
                };
        }).ToList();


            var result = new ShowResultToTeacherDTO
            {
                ExamId = exam.ID,
                ExamTitle = exam.Title,
                StudentResults = studentResults,
                TotalStudents = studentResults.Count()
            };
            return ResponseDTO<ShowResultToTeacherDTO>.Success(result);
        }
    }

}
