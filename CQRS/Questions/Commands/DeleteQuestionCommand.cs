
namespace StudentExamSystem.CQRS.Questions.Commands
{
    public class DeleteQuestionCommand : IRequest<bool>
    {
        public int QuestionId { get; set; }
    }
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepository<Question> repository;

        public DeleteQuestionCommandHandler(IMediator mediator, IGeneralRepository<Question> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            Question ExistsQuestion = await mediator.Send(new GetQuestionByIdQuery() { Id = request.QuestionId });
            if (ExistsQuestion != null)
            {
                repository.Delete(ExistsQuestion);
                repository.Save();
                return true;
            }
            return false;
        }
    }

}
