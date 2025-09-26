// DTO para devolver datos de usuario datos sensibles como password_hash no se incluyen

public class UsersDto
{
    public int id { get; set; }
    public string email { get; set; } = string.Empty;
    public string first_name { get; set; } = string.Empty;
    public string last_name { get; set; } = string.Empty;
    public string? phone { get; set; }
    public string? curp { get; set; }
    public DateTime? birth_date { get; set; }
    public string? address { get; set; }
    public string user_type { get; set; } = "YOUTH";
    public bool is_active { get; set; } = true;
    public DateTime? created_at { get; set; } = DateTime.UtcNow;
}