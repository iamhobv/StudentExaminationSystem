
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class ShowAllExamsToTeacherQuery:IRequest<ResponseDTO<List<ShowAllExamToTeacherDTO>>>
    {

    }
    public class ShowAllExamToTeacherQueryHandler : IRequestHandler<ShowAllExamsToTeacherQuery, ResponseDTO<List<ShowAllExamToTeacherDTO>>>
    {
        private readonly IGeneralRepository<Exam> repository;

        public ShowAllExamToTeacherQueryHandler(IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }

        public Task<ResponseDTO<List<ShowAllExamToTeacherDTO>>> Handle(ShowAllExamsToTeacherQuery request, CancellationToken cancellationToken)
        {
            var GetExams = repository
                .GetFilter(e => e.IsDeleted == false);

            var result = GetExams.Map<List<ShowAllExamToTeacherDTO>>();


            return Task.FromResult(ResponseDTO<List<ShowAllExamToTeacherDTO>>.Success(result,""));

        }
    }

}
