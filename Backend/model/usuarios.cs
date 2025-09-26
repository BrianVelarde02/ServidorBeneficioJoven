// Modelo que representa la tabla Users en la base de datos
public class Users
{
    public int id { get; set; }  // id INT (Primary Key)

    public string correo { get; set; } = string.Empty; // VARCHAR(255)

    public string hash_contrasena { get; set; } = string.Empty; // VARCHAR(255)

    public string nombre { get; set; } = string.Empty; // VARCHAR(100)

    public string apellidos { get; set; } = string.Empty; // VARCHAR(150)

    public string? telefono { get; set; } // VARCHAR(30) - puede ser null

    public string? curp { get; set; } // CHAR(18)

    public string? direccion { get; set; } // VARCHAR(255)

    public string tipo_usuario { get; set; } = "JOVEN"; // VARCHAR(20) - valores posibles: "JOVEN", "NEGOCIO", "ADMIN"

    public bool esta_activo { get; set; } = true; // TINYINT(1) → bool

    public DateTime creado_en { get; set; } = DateTime.UtcNow; // TIMESTAMP
}