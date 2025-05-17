using MediatR;
using StudentExamSystem.Data;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.MCQOtions.Commands
{
    public class AddMCQOptionsCommand : IRequest<bool>
    {
        public int QuestionID { get; set; }


        public List<string> Options { get; set; }
    }
    public class AddMCQOptionsCommandHandler : IRequestHandler<AddMCQOptionsCommand, bool>
    {
        private readonly IGeneralRepository<MCQAnswerOptions> repository;

        public AddMCQOptionsCommandHandler(IGeneralRepository<MCQAnswerOptions> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(AddMCQOptionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<MCQAnswerOptions> mCQAnswerOptions = new List<MCQAnswerOptions>();
                foreach (var option in request.Options)
                {
                    mCQAnswerOptions.Add(new MCQAnswerOptions { QuestionID = request.QuestionID, Option = option });
                }

                repository.AddList(mCQAnswerOptions);
                repository.Save();
                
                return Task.FromResult(true);
            }
            catch (Exception)
            {

                return Task.FromResult(false);
            }

        }
    }
}
