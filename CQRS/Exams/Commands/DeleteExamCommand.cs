
namespace StudentExamSystem.CQRS.Exams.Commands
{
    public class DeleteExamCommand:IRequest<bool>
    {
        public int Id { get; }
        public DeleteExamCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteExamCommandandler : IRequestHandler<DeleteExamCommand, bool>
    {
        private readonly IGeneralRepository<Exam> repository;

        public DeleteExamCommandandler(IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Exam = repository.GetFilter(e => e.ID == request.Id).FirstOrDefault();

                if (Exam == null)
                    throw new KeyNotFoundException("Exam not found");
                Exam.IsDeleted = true;
                repository.Update(Exam);
                repository.Save();
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }


    
        }
    }
}
