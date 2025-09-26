// var builder = WebApplication.CreateBuilder(args);

// // 🔧 Registrar servicios
// builder.Services.AddControllers(); // Habilita controladores
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // 🔌 Inyectar tu lógica de negocio
// var rutaCadeteria = Path.Combine("Datos", "cadeteria.json");
// var rutaCadetes = Path.Combine("Datos", "cadetes.json");

// var acceso = new AccesoADatosJson(); // o AccesoADatosCSV
// var rutaPedidos = Path.Combine("Datos", "pedidos.json");
// var cadeteria = new Cadeteria(acceso, rutaCadeteria, rutaCadetes, rutaPedidos);

// builder.Services.AddSingleton(cadeteria);

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.UseAuthorization(); 

// app.MapControllers(); 

// app.Run();
var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(CadeteriaCargar.CrearInstancia());

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
