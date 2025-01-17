using RealWorldSharp.Data;
using Microsoft.EntityFrameworkCore;
using RealWorldSharp.Repos;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	Args = args,
	WebRootPath = "StaticFiles",
});

builder.Services
	.AddCors(policy => policy
	.AddDefaultPolicy(builder => builder
	.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod()));

builder.AddAuth();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddDbContext<RealWorldContext>(
	o =>
	{
		o.UseInMemoryDatabase("RealWorldDB");
	}
);


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RealWorldService>();

var app = builder.Build();

// for demo
using (var scope = app.Services.CreateScope())
{
	using var context = scope.ServiceProvider.GetService<RealWorldContext>();
	Utils.SeedContext(context!);
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();
app.UseAuth();

app.MapEndpoints();

app.Run();

