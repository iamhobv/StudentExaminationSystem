
namespace StudentExamSystem.CQRS.Questions.Commands
{
    public class AddQuestionCommand : IRequest<int>
    {
        public CreateQuestionDTO Question { get; set; }
    }
    public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, int>
    {
        private readonly IGeneralRepository<Question> repository;

        public AddQuestionCommandHandler(IGeneralRepository<Question> repository)
        {
            this.repository = repository;
        }
        public Task<int> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                Question question = request.Map<Question>();
                question.CreatedAt = DateTime.UtcNow;
                question.IsDeleted = false;
                repository.Add(question);
                repository.Save();
                return Task.FromResult(question.ID);
            }
            catch (Exception)
            {
                return Task.FromResult(-1);
                throw;
            }

        }
    }
}
