using AutoMapper;

namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class ExamProfile:Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, AddExamDTO>()
                .ForMember(dest => dest.ExamQuestionsIDs,
                opt => opt.MapFrom(src => src.ExamQuestions.Select(q => q.ID)))
                .ReverseMap();
            //.ForMember(dest=>dest.ExamQuestions,opt=>opt.Ignore());


            CreateMap<updateExamDTO, Exam>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.ExamQuestions, opt => opt.Ignore());


            CreateMap<Exam, ExamDTO>()
           .ForMember(dest => dest.QuestionIds,
                    opt => opt.MapFrom(src => src.ExamQuestions.Select(eq => eq.QuestionID)));
        }
    }
}
