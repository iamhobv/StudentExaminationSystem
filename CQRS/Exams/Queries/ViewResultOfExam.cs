using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.Student;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class ViewResultOfExam : IRequest<ExamResultDTO>
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
    }
    public class ViewResultOfExamHandler : IRequestHandler<ViewResultOfExam, ExamResultDTO>
    {
        private readonly IGeneralRepository<StudentAnswer> studentRepo;
        private readonly IGeneralRepository<Exam> ExamRepo;
        private readonly IMediator mediator;


        public ViewResultOfExamHandler(IGeneralRepository<StudentAnswer> studentRepo,IGeneralRepository<Exam> ExamRepo, IMediator mediator)
        {
            this.studentRepo = studentRepo;
            this.ExamRepo = ExamRepo;
            this.mediator = mediator;
        }
        public async Task<ExamResultDTO> Handle(ViewResultOfExam request, CancellationToken cancellationToken)
        {
            var exam = await ExamRepo.GetAll()
              .Include(e => e.ExamQuestions)
              .ThenInclude(eq => eq.Question)
             .FirstOrDefaultAsync(e => e.ID == request.ExamId);

            if (exam == null)
            {
                return new ExamResultDTO();
            }

            var studentAnswers = studentRepo.GetAll().Where(s=>s.ExamID== request.ExamId&& s.StudentID == request.StudentId).Select(s=>new StudentAnswerDTO
            {
                QuestionID = s.QuestionID,
                StudentQuestionAnswer = s.StudentQuestionAnswer
            }).ToList();

            var totalScore = exam.ExamQuestions
            .Where(eq => eq.Question != null)
            .Sum(eq =>
            {
                var studentAnswer = studentAnswers
                 .FirstOrDefault(s => s.QuestionID == eq.QuestionID)?
                .StudentQuestionAnswer
                .Trim();

                if (studentAnswer != null &&
                    string.Equals(studentAnswer, eq.Question.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return eq.Question.QuestionMark;
                }

                return 0;
            });


            List<GetQuestionDTO> getQuestions = new List<GetQuestionDTO>();
            foreach (var item in exam.ExamQuestions)
            {
                var res = await mediator.Send(new GetExamQuestionsQuery() { ID = item.QuestionID });
                if (res != null)
                {
                    getQuestions.Add(res);
                }

            }

            var result = new ExamResultDTO()
            { 
                Title = exam.Title,
                Duration = exam.Duration,
                TotalScore = totalScore,
                questions = exam.ExamQuestions.Select(eq => new QuestionDTO
                {
                    ID = eq.Question.ID,
                    QuestionBody = eq.Question.QuestionBody,
                    QuestionType = eq.Question.QuestionType,
                    QuestionMark = eq.Question.QuestionMark,
                    CorrectAnswer = eq.Question.CorrectAnswer,
                    Options = getQuestions.FirstOrDefault(q=>q.QuestionId== eq.QuestionID)?.Options,
                    StudentQuestionAnswer = studentAnswers.FirstOrDefault(s => s.QuestionID == eq.QuestionID)?.StudentQuestionAnswer
                }).ToList(),

            };
            return result;
        }
    }

}
