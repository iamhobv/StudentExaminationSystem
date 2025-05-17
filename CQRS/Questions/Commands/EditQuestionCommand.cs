using MediatR;
using StudentExamSystem.CQRS.Questions.Queries;
using StudentExamSystem.Data;
using StudentExamSystem.Models;
using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.CQRS.Questions.Commands
{
    public class EditQuestionCommand : IRequest<bool>
    {
        public int QeustionId { get; set; }
        public string? QuestionBody { get; set; }
        public int? QuestionMark { get; set; }
        public string? CorrectAnswer { get; set; }
    }
    public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepository<Question> repository;

        public EditQuestionCommandHandler(IMediator mediator, IGeneralRepository<Question> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }
        public async Task<bool> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            Question ExistsQuestion = await mediator.Send(new GetQuestionByIdQuery() { Id = request.QeustionId });
            if (ExistsQuestion != null)
            {
                ExistsQuestion.QuestionBody = request.QuestionBody ?? ExistsQuestion.QuestionBody;
                ExistsQuestion.QuestionMark = request.QuestionMark ?? ExistsQuestion.QuestionMark;
                ExistsQuestion.CorrectAnswer = request.CorrectAnswer ?? ExistsQuestion.CorrectAnswer;

                repository.Update(ExistsQuestion);
                repository.Save();
                return true;
            }
            return false;

            //throw new NotImplementedException();
        }
    }
}
