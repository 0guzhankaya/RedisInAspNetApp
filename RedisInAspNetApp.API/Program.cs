using Microsoft.EntityFrameworkCore;
using RedisInAspNetApp.API.Models;
using RedisInAspNetApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseInMemoryDatabase("myDatabase");
});

var app = builder.Build();

// In-MemoryDb'yi uygulama her aya�a kalkt���nda s�f�rdan olu�turur.
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated();
}

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
