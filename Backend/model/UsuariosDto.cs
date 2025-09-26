// DTO para devolver datos de usuario datos sensibles como password_hash no se incluyen

public class UsersDto
{
    public int id { get; set; }
    public string correo { get; set; } = string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string apellidos { get; set; } = string.Empty;
    public string? telefono { get; set; }
    public string? curp { get; set; }
    public string? direccion { get; set; }
    public string tipo_usuario { get; set; } = "JOVEN";
    public bool esta_activo { get; set; } = true;
    public DateTime? creado_en { get; set; } = DateTime.UtcNow;
}