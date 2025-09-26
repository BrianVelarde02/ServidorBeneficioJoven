// Modelo que representa la tabla Users en la base de datos
public class Users
{
    public int id { get; set; }  // id INT (Primary Key)

    public string email { get; set; } = string.Empty; // VARCHAR(255)

    public string password_hash { get; set; } = string.Empty; // VARCHAR(255)

    public string first_name { get; set; } = string.Empty; // VARCHAR(100)

    public string last_name { get; set; } = string.Empty; // VARCHAR(150)

    public string? phone { get; set; } // VARCHAR(30) - puede ser null

    public string? curp { get; set; } // CHAR(18)

    public DateTime? birth_date { get; set; } // DATE

    public string? address { get; set; } // VARCHAR(255)

    public string user_type { get; set; } = "YOUTH"; // VARCHAR(20) - valores posibles: "YOUTH", "ADMIN"

    public bool is_active { get; set; } = true; // TINYINT(1) → bool

    public DateTime created_at { get; set; } = DateTime.UtcNow; // TIMESTAMP
}