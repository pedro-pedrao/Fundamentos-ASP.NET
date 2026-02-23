using Todo.Data;


//Criação do builder que vai carregar todos os métodos para dar vida e "infra" para a API
var builder = WebApplication.CreateBuilder(args);

//Adiciona todas as controllers presentes no sistema
builder.Services.AddControllers();

//Declara o AppDbContext/Banco como um serviço da aplicacao que pode ser chamado em qualquer parte dela (normalmente controllers)
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//Adiciona todas as controllers presentes no sistema
app.MapControllers();

app.Run();
