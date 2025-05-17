global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.IdentityModel.Tokens;
global using StudentExamSystem.Data;
global using StudentExamSystem.DTOs.AccountDTOs;
global using StudentExamSystem.Models;

global using MediatR;
global using StudentExamSystem.CQRS.Questions.Commands;
global using StudentExamSystem.CQRS.Questions.Orchesterator;
global using StudentExamSystem.CQRS.Questions.Queries;
global using StudentExamSystem.DTOs.QuestionDTOs;
global using StudentExamSystem.Services;
global using static StudentExamSystem.Enums.QuestionType;