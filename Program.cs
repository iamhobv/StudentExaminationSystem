
using System.Diagnostics;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentExamSystem.CQRS.Exams.Orchesterator;
using StudentExamSystem.Data;
using StudentExamSystem.Models;
using StudentExamSystem.Services;

namespace StudentExamSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<IGeneralRepository<Question>, GeneralRepository<Question>>();
            builder.Services.AddScoped<IGeneralRepository<MCQAnswerOptions>, GeneralRepository<MCQAnswerOptions>>();
            builder.Services.AddScoped<IGeneralRepository<Exam>, GeneralRepository<Exam>>();

            builder.Services.AddScoped<IGeneralRepository<Exam>, GeneralRepository<Exam>>();
            builder.Services.AddScoped<IGeneralRepository<ExamQuestion>, GeneralRepository<ExamQuestion>>();
            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));

            //builder.Services.AddScoped<IGeneralRepository<UpdateExamAddQuestionOrchesterator>, GeneralRepository<UpdateExamAddQuestionOrchesterator>>();





            builder.Services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineCS"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information);
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DataBaseContext>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = builder.Configuration["JWT:Iss"],
                        ValidateAudience = false,
                        ValidAudience = builder.Configuration["JWT:Aud"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    };
                });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddMediatR(opts =>
           opts.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            MapperServices.Mapper = app.Services.GetService<IMapper>();

            app.UseCors("AllowAll");


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
