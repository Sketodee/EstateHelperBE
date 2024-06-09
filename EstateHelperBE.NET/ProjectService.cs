using EstateHelper.Application.Auth;
using EstateHelper.Application.ConsultantGroups;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Application.Products;
using EstateHelper.Domain.ConsultantGroups;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Products;
using EstateHelper.Domain.User;
using EstateHelper.EntityFramework.Repository;

namespace EstateHelperBE.NET
{
    public static class ProjectService
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddHttpContextAccessor();
            services.AddScoped<Helpers>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IConsultantGroupAppService, ConsultantGroupAppService>();
            services.AddScoped<IConsultantGroupRepository, ConsultantGroupRepository>();
            services.AddScoped<IConsultantGroupManager, ConsultantGroupManager>();

            services.AddScoped<IProductAppService, ProductAppService>();    
            services.AddScoped<IProductRepository, ProductRepository>();    
            services.AddScoped<IProductManager, ProductManager>();  

        }
    }
}
