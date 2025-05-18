using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentExamSystem.CQRS.Exams.Queries;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.QuestionDTOs;

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
                return NotFound(new GeneralResponse
                {
                    IsPass = false
                });
            }
            return Ok(new GeneralResponse
            {
                IsPass = true,
                Data = exam
            });
        }


        #endregion
        #region view Exam
        [HttpGet("viewResult/{examId:int}/{studentId:int}")]
        public async Task<ActionResult<GeneralResponse>> ViewResult(int examId,string studentId)
        {
            var result = await mediator.Send(new ViewResultOfExam { ExamId = examId,StudentId = studentId});
            if (result == null)
            {

                return NotFound(new GeneralResponse
                {
                    IsPass= false
                });
            }

            return Ok(new GeneralResponse
            {
                IsPass = true,
                Data= result
            });

        }


        #endregion

    }
}
