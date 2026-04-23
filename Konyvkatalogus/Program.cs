using System.Reflection;
using Konyvkatalogus.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─── Adatbázis kapcsolat (MySQL / MariaDB) ────────────────────────────────────
// A kapcsolati sztring az appsettings.json-ból kerül importálásra
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Nincs 'DefaultConnection' kapcsolati sztring az appsettings.json-ban.");

builder.Services.AddDbContext<KonyvContext>(options =>
    options.UseMySQL(connectionString));

// ─── Kontrollerek ─────────────────────────────────────────────────────────────
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Egyéni hibakezelés validáció esetén
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new Microsoft.AspNetCore.Mvc.ValidationProblemDetails(context.ModelState)
            {
                Title = "Hibás kérés – validációs hiba",
                Status = 400
            };
            return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(problemDetails);
        };
    });

// ─── CORS konfiguráció ────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()   // Fejlesztési célra; élesben pontosítani kell
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ─── Swagger / OpenAPI ────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Könyvkatalógus REST API",
        Version = "v1",
        Description = "CRUD műveletek könyvek kezelésére MySQL adatbázisban. " +
                      "Végpontok: /adatHozzad (POST), /adatKiir (GET), " +
                      "/adatFrissit/{id} (PUT/PATCH), /adatTorles/{id} (DELETE)."
    });

    // XML kommentek beolvasása a Swagger dokumentációhoz
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

// ─── App felépítése ───────────────────────────────────────────────────────────
var app = builder.Build();

// Fejlesztési módban Swagger UI elérhetővé tétele
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Könyvkatalógus API v1");
        c.RoutePrefix = "swagger"; // Elérhető: https://localhost:{port}/swagger
    });
}

// ─── Middleware pipeline ──────────────────────────────────────────────────────
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ─── Automatikus migráció alkalmazása indításkor ──────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KonyvContext>();
    db.Database.Migrate();
}

app.Run();