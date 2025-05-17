using MediatR;
using StudentExamSystem.CQRS.MCQOtions.Commands;
using StudentExamSystem.CQRS.Questions.Commands;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.QuestionDTOs;

namespace StudentExamSystem.CQRS.Questions.Orchesterator
{
    public class AddMCQQuestionOrchesterator : IRequest<bool>
    {
        public CreateQuestionDTO Question { get; set; }
        public List<string> Options { get; set; }
    }

    public class AddMCQQuestionOrchesteratorHandler : IRequestHandler<AddMCQQuestionOrchesterator, bool>
    {
        private readonly IMediator mediator;

        public AddMCQQuestionOrchesteratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddMCQQuestionOrchesterator request, CancellationToken cancellationToken)
        {
            int questionId = await mediator.Send(new AddQuestionCommand() { Question = request.Question });
            if (questionId != -1)
            {
                var res = await mediator.Send(new AddMCQOptionsCommand() { QuestionID = questionId, Options = request.Options });
                return true;
            }
            return false;
        }
    }
}
