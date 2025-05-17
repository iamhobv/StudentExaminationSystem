using StudentExamSystem.DTOs.ExamDTOs;

namespace StudentExamSystem.CQRS.Exams.Commands
{
    public class AddExamCommand:IRequest<AddExamDTO>
    {

    }

    public class AddExamCommandHandler : IRequestHandler<AddExamCommand, AddExamDTO>
    {
        private readonly IGeneralRepository<Exam> repository;

        public AddExamCommandHandler(IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }

        public Task<AddExamDTO> Handle(AddExamCommand request, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch(Exception ex)
            {

            } 
        }
    }
}
