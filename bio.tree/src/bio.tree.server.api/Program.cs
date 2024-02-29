using bio.tree.server.infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseInfrastructure();
app.MapControllers();
app.Run();

