using Microsoft.EntityFrameworkCore;
using WasmMP2.Api.Data;

var builder = WebApplication.CreateBuilder(args);

//dbContext com sqlite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=wasmmp2.db";
    options.UseSqlite(cs);
});

//CORS para ambiente de dev
var corsPolicyName = "AllowWasmClient";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        //ajustar as URLs conforme a porta Client
        policy.WithOrigins("http://localhost:5148", "https://localhost:5148").AllowAnyHeader().AllowAnyMethod();
    });
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Aplicar o seedService na inicialização
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Habilita o CORS   
app.UseCors(corsPolicyName);

//GET /api/categories
app.MapGet("/api/categories", async (AppDbContext db) => await db.Categories.AsNoTracking().ToListAsync());

//GET /api/products
app.MapGet("/api/products", async (AppDbContext db) => await db.Products.Include(p => p.Category).AsNoTracking().ToListAsync());

app.Run();