using VladimirNyaninKT_31_22.Interfaces;


namespace VladimirNyaninKT_31_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
            return services;
        }

    }
}
