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

        //[HttpPost("addQuestion")]
        //public async Task<ActionResult<GeneralResponse>> AddQeustion(AddQeustionDTO qeustionDTO)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (qeustionDTO.QuestionType == QuestionTypes.TF)
        //        {
        //            CreateQuestionDTO createQuestion = qeustionDTO.Map<CreateQuestionDTO>();
        //            var res = await mediator.Send(new AddQuestionCommand() { Question = createQuestion });
        //            if (res != -1)
        //            {
        //                return new GeneralResponse()
        //                {
        //                    IsPass = true,
        //                    Data = "Q added"
        //                };
        //            }
        //            return new GeneralResponse()
        //            {
        //                IsPass = false,
        //                Data = "Error"
        //            };
        //        }
        //        else if (qeustionDTO.QuestionType == QuestionTypes.MCQ)
        //        {
        //            CreateQuestionDTO createQuestion = qeustionDTO.Map<CreateQuestionDTO>();

        //            var res = await mediator.Send(new AddMCQQuestionOrchesterator() { Question = createQuestion, Options = qeustionDTO.Options });
        //            if (res)
        //            {

        //                return new GeneralResponse()
        //                {
        //                    IsPass = true,
        //                    Data = "Q added"
        //                };
        //            }
        //            return new GeneralResponse()
        //            {
        //                IsPass = false,
        //                Data = "Error"
        //            };
        //        }
        //    }
        //    return new GeneralResponse()
        //    {
        //        IsPass = false,
        //        Data = ModelState
        //    };
        //}


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
                        return Ok(new GeneralResponse()
                        {
                            IsPass = true,
                            Data = res
                        });
                    }

                    return BadRequest(new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Error"
                    });
                }

                else if (qeustionDTO.QuestionType == QuestionTypes.MCQ)
                {
                    CreateQuestionDTO createQuestion = qeustionDTO.Map<CreateQuestionDTO>();

                    var res = await mediator.Send(new AddMCQQuestionOrchesterator()
                    {
                        Question = createQuestion,
                        Options = qeustionDTO.Options
                    });

                    if (res != -1)
                    {
                        return Ok(new GeneralResponse()
                        {
                            IsPass = true,
                            Data = res
                        });
                    }

                    return BadRequest(new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Error"
                    });
                }
            }

            return BadRequest(new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            });
        }

        [HttpGet("ExamQuestions")]
        public async Task<ActionResult<GeneralResponse>> GetAllQeustions()
        {
            var res = await mediator.Send(new GetAllQuestionsQuesry());
            return new GeneralResponse()
            {
                IsPass = true,
                Data = res
            };
        }


        [HttpGet("TeacherQuestions")]
        public async Task<ActionResult<GeneralResponse>> GetAllQeustionsForTeachers()
        {
            var res = await mediator.Send(new GetQuestionsForTeachers());
            return new GeneralResponse()
            {
                IsPass = true,
                Data = res
            };
        }

        [HttpPut("EditQuestion")]
        public async Task<ActionResult<GeneralResponse>> EditQuestion(EditQeustionDTO editQeustion)
        {
            if (ModelState.IsValid)
            {
                EditQuestionCommand questionCommand = editQeustion.Map<EditQuestionCommand>();

                var res = await mediator.Send(questionCommand);
                if (res)
                {
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "question updated"
                    };
                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "wronge question selected"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }

        [HttpDelete("DeleteQeustion/{QuestionId:int}")]
        public async Task<ActionResult<GeneralResponse>> DeleteQeustion(int QuestionId)
        {


            var res = await mediator.Send(new DeleteQuestionCommand() { QuestionId = QuestionId });
            if (res)
            {
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = "question deleted"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = "wronge question selected"
            };
        }


    }
}
