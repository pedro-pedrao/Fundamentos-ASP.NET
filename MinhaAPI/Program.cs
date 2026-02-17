var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/TesteComMaisUm", () => "Deu certo!");
app.MapGet("/{nome}", (string nome) =>
{
    return Results.Ok($"Olá {nome}");
});

app.MapPost("/", (User user) =>
{
     return Results.Ok(user);
});

app.Run();


public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}