using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;

var builder = WebApplication.CreateBuilder(args);

// --------------------- Configuración de servicios ---------------------

// Configurar conexión a MySQL
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

// Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------- Middleware ---------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --------------------- Endpoints ---------------------

app.MapGet("/", () => Results.Redirect("/inicio"));

app.MapGet("/inicio", () => "¡Bienvenido a Inicio!");

// Obtener todos los usuarios
app.MapGet("/usuarios", async (ApiDbContext db) =>
{
    var usuarios = await db.usuarios
        .Select(u => new UsersDto
        {
            id = u.id,
            correo = u.correo,
            nombre = u.nombre,
            apellidos = u.apellidos,
            telefono = u.telefono,
            curp = u.curp,
            direccion = u.direccion,
            tipo_usuario = u.tipo_usuario,
            esta_activo = u.esta_activo,
            creado_en = u.creado_en
        })
        .ToListAsync();

    return Results.Ok(usuarios);
})
.WithName("ObtenerUsuarios");

// Obtener usuario por ID
app.MapGet("/usuario/{id}", async (ApiDbContext db, int id) =>
{
    var user = await db.usuarios
        .Where(u => u.id == id)
        .Select(u => new UsersDto
        {
            id = u.id,
            correo = u.correo,
            nombre = u.nombre,
            apellidos = u.apellidos,
            telefono = u.telefono,
            curp = u.curp,
            direccion = u.direccion,
            tipo_usuario = u.tipo_usuario,
            esta_activo = u.esta_activo,
            creado_en = u.creado_en
        })
        .FirstOrDefaultAsync();

    return user is not null
        ? Results.Ok(user)
        : Results.NotFound(new { mensaje = "Usuario no encontrado" });
})
.WithName("ObtenerUsuarioPorId");

// Eliminar usuario
app.MapDelete("/usuario/{id:int}", async (ApiDbContext db, int id) =>
{
    var usuario = await db.usuarios.FindAsync(id);
    if (usuario == null)
        return Results.NotFound(new { mensaje = "Usuario no encontrado" });

    db.usuarios.Remove(usuario);
    await db.SaveChangesAsync();
    return Results.Ok(new { mensaje = $"Usuario {usuario.nombre} {usuario.apellidos} eliminado correctamente" });
})
.WithName("EliminarUsuario");

// --------------------- Crear Usuario ---------------------
app.MapPost("/usuario", async (ApiDbContext db, UsersCreateDto dto) =>
{
    // Validaciones
    if (string.IsNullOrWhiteSpace(dto.nombre) || string.IsNullOrWhiteSpace(dto.curp))
        return Results.BadRequest(new { mensaje = "Nombre y CURP son obligatorios" });

    if (await db.usuarios.AnyAsync(u => u.correo == dto.correo))
        return Results.BadRequest(new { mensaje = "Email ya registrado" });

    if (!string.IsNullOrWhiteSpace(dto.curp) && await db.usuarios.AnyAsync(u => u.curp == dto.curp))
        return Results.BadRequest(new { mensaje = "CURP ya registrado" });

    // Hashear contraseña con Argon2id
    string passwordHash = Argon2.Hash(dto.contrasena);

    var user = new Users
    {
        correo = dto.correo,
        hash_contrasena = passwordHash,
        nombre = dto.nombre,
        apellidos = dto.apellidos,
        telefono = dto.telefono,
        curp = dto.curp,
        direccion = dto.direccion,
        tipo_usuario = dto.tipo_usuario,
        esta_activo = true
    };

    db.usuarios.Add(user);
    await db.SaveChangesAsync();

    var resultDto = new UsersDto
    {
        id = user.id,
        correo = user.correo,
        nombre = user.nombre,
        apellidos = user.apellidos,
        telefono = user.telefono,
        curp = user.curp,
        direccion = user.direccion,
        tipo_usuario = user.tipo_usuario,
        esta_activo = user.esta_activo,
        creado_en = user.creado_en
    };

    return Results.Ok(resultDto);
});

// --------------------- Login ---------------------
app.MapPost("/login", async (ApiDbContext db, LoginDto login) =>
{
    var user = await db.usuarios.FirstOrDefaultAsync(u => u.correo == login.correo);

    if (user == null)
        return Results.NotFound(new { mensaje = "Usuario no encontrado" });

    if (string.IsNullOrWhiteSpace(user.tipo_usuario))
        user.tipo_usuario = "JOVEN";

    // Verificar contraseña usando Argon2
    bool passwordValida = Argon2.Verify(user.hash_contrasena, login.contrasena);

    if (!passwordValida)
        return Results.BadRequest(new { mensaje = "Contraseña incorrecta" });

    var result = new
    {
        id = user.id,
        correo = user.correo,
        nombre = user.nombre,
        apellidos = user.apellidos,
        telefono = user.telefono,
        curp = user.curp,
        direccion = user.direccion,
        tipo_usuario = user.tipo_usuario
    };

    return Results.Ok(result);
});

app.Run();
