using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Pizzéria.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddDbContext<PizzaEhodDB>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSqlite<PizzaEhodDB>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = " API Pizzéria ", Description = " Faire les pizzas que vous aimez ", Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", " Pizzéria API V1 ");
});

app.MapGet("/", () => " Ecole supérieure polytechnique DIC3 2025 ");
app.MapGet("/pizzas", async (PizzaEhodDB db) =>
{
    return await db.Pizzas.ToListAsync();
});
app.MapPost("/pizzas", async (PizzaEhodDB db, PizzaEhod pizza) =>
{
    db.Pizzas.Add(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizzas/{pizza.IdEhod}", pizza);
});
app.MapGet("/pizza/{id}", async (PizzaEhodDB db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizza/{id}", async (PizzaEhodDB db, PizzaEhod updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.NomEhod = updatepizza.NomEhod;
    pizza.DescriptionEhod = updatepizza.DescriptionEhod;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/pizza/{id}", async (PizzaEhodDB db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.Run();