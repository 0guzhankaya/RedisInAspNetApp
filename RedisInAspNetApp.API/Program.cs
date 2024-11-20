using Microsoft.EntityFrameworkCore;
using Redis;
using RedisInAspNetApp.API.Models;
using RedisInAspNetApp.API.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository>(sp =>
{
	var appDbContext = sp.GetRequiredService<AppDbContext>();
	var productRepository = new ProductRepository(appDbContext);
	var redisService = sp.GetRequiredService<RedisService>();

	return new ProductRepositoryWithCache(productRepository, redisService);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseInMemoryDatabase("myDatabase");
});

builder.Services.AddSingleton<RedisService>(sp =>
{
	return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(sp =>
{
	var redisService = sp.GetRequiredService<RedisService>();
	return redisService.GetDb(0);
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
