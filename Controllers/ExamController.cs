
using Microsoft.AspNetCore.Authorization;
using StudentExamSystem.CQRS.Exams.Commands;
using StudentExamSystem.CQRS.Exams.Orchesterator;
using StudentExamSystem.CQRS.Exams.Queries;

namespace StudentExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExamController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        #region Add New Exam
        [HttpPost] //api/Exam
        public async Task<IActionResult> AddExam(AddExamDTO addExam)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new AddExamCommand(addExam));
            return Ok(result);
        }
        #endregion

        //#region update Exam
        //[HttpPut("{id:int}")] //api/Exam
        //public async Task<IActionResult> UpdateExam(int id,updateExamDTO Exam)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await mediator.Send(new UpdateExamCommand(id, Exam) { id=id});
        //    return Ok(result);
        //}
        //#endregion

        #region update Exam
        //[Authorize]
        [HttpPut("{id:int}")] //api/Exam
        public async Task<IActionResult> UpdateExam(int id, updateExamDTO Exam)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await mediator.Send(new UpdateExamAddQuestionOrchesterator(Exam) );
            return Ok(result);
        }
        #endregion

        [HttpGet("results/{examId}")]
        public async Task<IActionResult> GetExamResultsForTeacher(int examId)
        {
            var response = await mediator.Send(new ShowResultExamToTeacher(examId));
            return Ok(response);
        }

    }
}
