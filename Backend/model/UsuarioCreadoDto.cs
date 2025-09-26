// DTO para crear un nuevo usuario 
public class UsersCreateDto
{
    public string correo { get; set; } = string.Empty;
    public string contrasena { get; set; } = string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string apellidos { get; set; } = string.Empty;
    public string? telefono { get; set; }
    public string? curp { get; set; }
    public string? direccion { get; set; }
    public string tipo_usuario { get; set; } = "JOVEN";
}