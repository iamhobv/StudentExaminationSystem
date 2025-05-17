using AutoMapper;
using StudentExamSystem.CQRS.Questions.Commands;
using StudentExamSystem.Models;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {

            CreateMap<Question, CreateQuestionDTO>().ReverseMap();
            CreateMap<AddQeustionDTO, CreateQuestionDTO>().ReverseMap();
            CreateMap<AddQuestionCommand, Question>()
                .ForMember(dst => dst.QuestionMark, opts => opts.MapFrom(src => src.Question.QuestionMark))
                .ForMember(dst => dst.QuestionBody, opts => opts.MapFrom(src => src.Question.QuestionBody))
                .ForMember(dst => dst.QuestionType, opts => opts.MapFrom(src => src.Question.QuestionType))
                .ForMember(dst => dst.CorrectAnswer, opts => opts.MapFrom(src => src.Question.CorrectAnswer))
                .ReverseMap();
            CreateMap<Question, GetQuestionDTO>()
                .ForMember(dst => dst.QuestionId, opts => opts.MapFrom(src => src.ID))
                .ReverseMap();
            CreateMap<Question, GetTeacherQuestionDTO>()
                .ForMember(dst => dst.QuestionId, opts => opts.MapFrom(src => src.ID))
                .ReverseMap();
            //CreateMap<MCQAnswerOptions, String>()
            //                    .ForMember(dst => dst., opts => opts.MapFrom(src => src.Option)).ReverseMap();


            CreateMap<EditQeustionDTO, EditQuestionCommand>().ReverseMap();

        }
    }
}
