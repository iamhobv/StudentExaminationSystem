﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentExamSystem.Data;
using StudentExamSystem.DTOs.Student;
using StudentExamSystem.Models;

namespace StudentExamSystem.CQRS.Exams.Queries
{
    public class GetAvailableExamsQuery: IRequest<List<GetAvailableExamDTO>>
    {
    }

    public class GetAvailableExamsHandler : IRequestHandler<GetAvailableExamsQuery, List<GetAvailableExamDTO>>
    {
        private readonly IGeneralRepository<Exam> repository;
        public GetAvailableExamsHandler(IGeneralRepository<Exam> repository)
        {
            this.repository = repository;
        }

        public async Task<List<GetAvailableExamDTO>> Handle(GetAvailableExamsQuery request, CancellationToken cancellationToken)
        {
            var Exams = await repository.GetAll().Where(e=>e.IsDeleted == false).Select(e => new GetAvailableExamDTO { 
                Id=e.ID,
                Title=e.Title,
                Duration=e.Duration
            }).ToListAsync();
            return Exams;
        }
    }
}
