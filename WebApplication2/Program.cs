using DiakKurzusApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext regisztráció SQLite adatbázissal, abszolút elérési úttal
builder.Services.AddDbContext<EgyetemContext>(options =>
{
    var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "egyetem.db");
    options.UseSqlite($"Data Source={dbPath}");
});

// Controllerek
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS engedélyezése (fejlesztéshez)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Migrációk alkalmazása indításkor
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EgyetemContext>();
    dbContext.Database.Migrate();
}

app.Run();