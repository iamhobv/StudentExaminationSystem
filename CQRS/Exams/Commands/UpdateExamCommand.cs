
namespace StudentExamSystem.CQRS.Exams.Commands
{
    public class UpdateExamCommand : IRequest<ResponseDTO<ExamDTO>>
    {
        public int id { get; set; }
        public updateExamDTO UpdateExam { get; }

        public UpdateExamCommand(int id, updateExamDTO updateExam)
        {
            this.id = id;
            UpdateExam = updateExam;
        }
    }
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, ResponseDTO<ExamDTO>>
    {
        private readonly IGeneralRepository<Exam> repository;

        public UpdateExamCommandHandler(IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }
        public Task<ResponseDTO<ExamDTO>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedExam = repository.GetByID(request.id);
                if (updatedExam == null)
                {
                    return Task.FromResult(ResponseDTO<ExamDTO>.Error(ErrorCode.NotFound, "Exam not found"));
                }

                request.UpdateExam.Map(updatedExam);

                repository.Update(updatedExam);
                repository.Save();

                var UpdateExamDto = updatedExam.Map<ExamDTO>();
                return Task.FromResult(ResponseDTO<ExamDTO>.Success(UpdateExamDto, "Exam Updated successfully"));

            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseDTO<ExamDTO>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}"));

            }

        }
    }
}
