using Microsoft.EntityFrameworkCore;
using MyMinimalAPI.Context;
using MyMinimalAPI.Model;
using MyMinimalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDB>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddTransient<TodoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/Todo", async (TodoModel todo, TodoService service) => {
    await service.Add(todo);
    return Results.Created($"/Todo/{todo.Id}", todo);
});

app.MapGet("/Todo", async (TodoService service) => await service.Get());

app.MapGet("/Todo/{id}", async (int id, TodoService service) =>
    await service.GetById(id) is TodoModel todo ? Results.Ok(todo) : Results.NotFound());

app.MapDelete("/Todo/{id}", async (int id, TodoService service) =>
    await service.Delete(id) is TodoModel todo ? Results.Ok(todo) : Results.NotFound());


app.Run();