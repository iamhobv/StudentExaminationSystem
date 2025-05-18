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
                var existingExam = repository.GetByID(request.id);

                if (existingExam == null)
                {
                    return Task.FromResult(ResponseDTO<ExamDTO>.Error(ErrorCode.NotFound, "Exam not found"));
                }

                //request.UpdateExam.Map(existingExam);

                if (request.UpdateExam.Title != null)
                {
                    existingExam.Title = request.UpdateExam.Title;
                }

                if (request.UpdateExam.Duration.HasValue)
                {
                    existingExam.Duration = request.UpdateExam.Duration.Value;
                }

                repository.Update(existingExam);
                repository.Save();

                var UpdateExamDto = existingExam.Map<ExamDTO>();

                return Task.FromResult(ResponseDTO<ExamDTO>.Success(UpdateExamDto, "Exam Updated successfully"));

            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseDTO<ExamDTO>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}"));

            }

        }
    }
}
