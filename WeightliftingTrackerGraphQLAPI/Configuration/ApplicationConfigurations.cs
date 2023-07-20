namespace WeightliftingTrackerGraphQLAPI.Configuration
{
    public static class ApplicationConfigurations
    {
        public static string GetConnectionString(WebApplicationBuilder app)
        {
            return app.Configuration.GetConnectionString("DefaultConnection");
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }
    }
}
