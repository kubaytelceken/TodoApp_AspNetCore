using AutoMapper;
using FluentValidation;
using KT.TodoAppNTier.Business.Interfaces;
using KT.TodoAppNTier.Business.Mappings.AutoMapper;
using KT.TodoAppNTier.Business.Services;
using KT.TodoAppNTier.Business.ValidationRules;
using KT.TodoAppNTier.DataAccess.Contexts;
using KT.TodoAppNTier.DataAccess.UnitOfWork;
using KT.TodoAppNTier.Dtos.WorkDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkServices, WorkServices>();
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer("server=(localdb)\\mssqllocaldb; Database=TodoDB;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True");
            });
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }
    }
}
