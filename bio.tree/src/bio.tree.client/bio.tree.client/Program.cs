using bio.tree.client.Components;
using bio.tree.client.Configuration;
using bio.tree.client.Services;
using bio.tree.client.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();