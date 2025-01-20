using RealWorldSharp.Data;
using Microsoft.EntityFrameworkCore;
using RealWorldSharp.Repos;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	Args = args,
	WebRootPath = "wwroot",
});

builder.Services
	.AddCors(policy => policy
		.AddDefaultPolicy(builder => builder
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod()
		)
	);

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

// for RealWorld demo
using (var scope = app.Services.CreateScope())
{
	using var context = scope.ServiceProvider.GetService<RealWorldContext>();
	Utils.SeedContext(context!);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();
app.UseAuth();

app.MapEndpoints();

app.Run();

