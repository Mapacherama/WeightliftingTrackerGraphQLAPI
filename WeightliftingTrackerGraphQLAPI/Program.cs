using WeightliftingTrackerGraphQLAPI.Configuration;
using WeightliftingTrackerGraphQLAPI.Extensions;
using WeightliftingTrackerGraphQLAPI.GraphQL;
using WeightliftingTrackerGraphQLAPI.GraphQL.Types;
using WeightliftingTrackerGraphQLAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registering services
builder.Services.AddDataServices(ApplicationConfigurations.GetConnectionString(builder));
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddResolvers();
builder.Services.AddRepositories();
builder.Services.AddScoped<Query>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

ApplicationConfigurations.ConfigureCors(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<WorkoutInputType>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("Open");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL();
});

await app.RunAsync();
