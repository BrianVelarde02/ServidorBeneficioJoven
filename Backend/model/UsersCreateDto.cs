// DTO para crear un nuevo usuario 
public class UsersCreateDto
{
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string first_name { get; set; } = string.Empty;
    public string last_name { get; set; } = string.Empty;
    public string? phone { get; set; }
    public string? curp { get; set; }
    public DateTime? birth_date { get; set; }
    public string? address { get; set; }
    public string user_type { get; set; } = "YOUTH";
}