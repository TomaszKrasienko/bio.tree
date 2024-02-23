using bio.tree.server.infrastructure.Configuration;
using bio.tree.shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/test", () => Results.Ok(new TestDto()
{
    Text = "Hello web api!"
}));

app.Run();

