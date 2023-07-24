using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Repositories;
using WeightliftingTrackerGraphQLAPI.Resolvers;

namespace WeightliftingTrackerGraphQLAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            services.AddScoped<IMySqlDataAccess>(x => new MySqlDataAccess(connectionString));
            return services;
        }

        public static IServiceCollection AddResolvers(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<WorkoutResolvers>();
            services.AddScoped<UserResolvers>();
            services.AddScoped<NutritionResolvers>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<INutritionRepository, NutritionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
