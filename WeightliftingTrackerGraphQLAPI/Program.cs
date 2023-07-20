using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.GraphQL;
using WeightliftingTrackerGraphQLAPI.GraphQL.Types;
using WeightliftingTrackerGraphQLAPI.Mappings;
using WeightliftingTrackerGraphQLAPI.Repositories;
using WeightliftingTrackerGraphQLAPI.Resolvers;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registering services
builder.Services.AddScoped<IMySqlDataAccess>(x => new MySqlDataAccess(connectionString));
builder.Services.AddScoped<WorkoutResolvers>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<Query>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<WorkoutInputType>(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL();
});

app.Run();
