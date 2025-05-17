using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentExamSystem.CQRS.Questions.Commands;
using StudentExamSystem.CQRS.Questions.Orchesterator;
using StudentExamSystem.CQRS.Questions.Queries;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.QuestionDTOs;
using StudentExamSystem.Services;
using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuestionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("addQuestion")]
        public async Task<ActionResult<GeneralResponse>> AddQeustion(AddQeustionDTO qeustionDTO)
        {
            if (ModelState.IsValid)
            {

                if (qeustionDTO.QuestionType == QuestionTypes.TF)
                {
                    CreateQuestionDTO createQuestion = qeustionDTO.Map<CreateQuestionDTO>();
                    var res = await mediator.Send(new AddQuestionCommand() { Question = createQuestion });
                    if (res != -1)
                    {

                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Q added"
                        };
                    }
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Error"
                    };
                }
                else if (qeustionDTO.QuestionType == QuestionTypes.MCQ)
                {
                    CreateQuestionDTO createQuestion = qeustionDTO.Map<CreateQuestionDTO>();

                    var res = await mediator.Send(new AddMCQQuestionOrchesterator() { Question = createQuestion, Options = qeustionDTO.Options });
                    if (res)
                    {

                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Q added"
                        };
                    }
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Error"
                    };
                }
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }

        [HttpGet("Questions")]

        public async Task<ActionResult<GeneralResponse>> GetAllQeustions()
        {
            var res = await mediator.Send(new GetAllQuestionsQuesry());
            return new GeneralResponse()
            {
                IsPass = true,
                Data = res
            };
        }
    }
}
