using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Mapping;
using MyStreeTBackend.Repo;
using MyStreeTBackend.Repo.RepoImpl;
using MyStreeTBackend.Service;
using MyStreeTBackend.Service.ServiceImpl;

namespace MyStreeTBackend.Global
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoriesService(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //Services
            services.AddScoped<IuserService, UserService>();


            //AutoMapper
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}