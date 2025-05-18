using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.QuestionDTOs;
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


        public ViewResultOfExamHandler(IGeneralRepository<StudentAnswer> studentRepo,IGeneralRepository<Exam> ExamRepo)
        {
            this.studentRepo = studentRepo;
            this.ExamRepo = ExamRepo;
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

            var studentAnswer = studentRepo.GetAll().Where(s=>s.ExamID== request.ExamId&& s.StudentID == request.StudentId).Select(s=>new StudentAnswerDTO
            {
                QuestionID = s.QuestionID,
                StudentQuestionAnswer = s.StudentQuestionAnswer
            }).ToList();

            var result = new ExamResultDTO()
            { 
                Title = exam.Title,
                Duration = exam.Duration,
                questions = exam.ExamQuestions.Select(eq => new QuestionDTO
                {
                    ID = eq.Question.ID,
                    QuestionBody = eq.Question.QuestionBody,
                    QuestionType = eq.Question.QuestionType,
                    QuestionMark = eq.Question.QuestionMark,
                    StudentQuestionAnswer = studentAnswer.FirstOrDefault(s => s.QuestionID == eq.QuestionID)?.ToString()
                }).ToList(),

            };
            return result;
        }
    }

}
