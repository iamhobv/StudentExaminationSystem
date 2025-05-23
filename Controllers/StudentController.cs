using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentExamSystem.CQRS.Exams.Queries;
using StudentExamSystem.CQRS.StudentAnswers.Orchesterator;
using StudentExamSystem.CQRS.StudentExams.Queries;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.Student;

namespace StudentExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator mediator;
        public StudentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region Get Available Exams
        [HttpGet("GetAvailableExams")]
        public async Task<ActionResult<GeneralResponse>> GetAvailableExams()
        {
            var exams = await mediator.Send(new GetAvailableExamsQuery());



            return Ok(new GeneralResponse
            {
                IsPass = true,
                Data = exams
            });
        }
        #endregion

        #region Take Exam
        [HttpGet("TakeExam/{id}")]
        public async Task<ActionResult<GeneralResponse>> TakeExam(int id)
        {
            var exam = await mediator.Send(new GetExamByIdQuery(id));
            if (exam == null)
            {
                return new GeneralResponse
                {
                    IsPass = false,
                    Data = "no exams"
                };
            }
            return Ok(new GeneralResponse
            {
                IsPass = true,
                Data = exam
            });
        }


        #endregion

        #region view Exam
        [HttpGet("viewResult/{examId:int}/{studentId}")]
        public async Task<ActionResult<GeneralResponse>> ViewResult(int examId, string studentId)
        {
            var result = await mediator.Send(new ViewResultOfExam { ExamId = examId, StudentId = studentId });
            if (result == null)
            {

                return NotFound(new GeneralResponse
                {
                    IsPass = false
                });
            }

            return Ok(new GeneralResponse
            {
                IsPass = true,
                Data = result
            });

        }
        #endregion

        #region Submit
        [HttpPost("submit")]
        public async Task<ActionResult<GeneralResponse>> Submit(SubmitExamDTO submitExamDTO)
        {
            var existsStdExam = await mediator.Send(new CheckIfStudentTakeThisExamBefore() { ExamID = submitExamDTO.ExamID, StdId = submitExamDTO.StudentID });
            if (existsStdExam)
            {
                bool result = await mediator.Send(new Submit() { ExamID = submitExamDTO.ExamID, StudentID = submitExamDTO.StudentID, StartedAt = submitExamDTO.StartedAt, SubmittedAt = submitExamDTO.SubmittedAt, StudentAnswerDTOList = submitExamDTO.StudentAnswerDTOList });
                if (result == true)
                {

                    return Ok(new GeneralResponse
                    {
                        IsPass = true,
                        Data = "subitted successfully"

                    });
                }
                return Ok(new GeneralResponse
                {
                    IsPass = false,
                    Data = "errors"
                });
            }
            return new GeneralResponse() { IsPass = false, Data = "This student take tgis exam before" };
        }
        #endregion


        [HttpGet("Check/{ExamID:int}/{StdId}")]
        public async Task<ActionResult<GeneralResponse>> Check(int ExamID, string StdId)
        {
            var existsStdExam = await mediator.Send(new CheckIfStudentTakeThisExamBefore() { ExamID = ExamID, StdId = StdId });
            if (existsStdExam)
            {
                return new GeneralResponse() { IsPass = true, Data = "This student can take this exam" };

            }
            return new GeneralResponse() { IsPass = false, Data = "This student take this exam before" };
        }


    }
}
