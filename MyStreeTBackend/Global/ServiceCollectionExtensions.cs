using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Mapping;
using MyStreeTBackend.Repo;
using MyStreeTBackend.Repo.RepoImpl;
using MyStreeTBackend.Service;
using MyStreeTBackend.Service.ServiceImpl;
using MyStreeTBackend.Utils;

namespace MyStreeTBackend.Global
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoriesService(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Services
            services.AddScoped<IuserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();

            //AutoMapper
            services.AddAutoMapper(typeof(UserProfile));

            //Token Service
            services.AddScoped<ITokenService, TokenService>();

        }
    }
}