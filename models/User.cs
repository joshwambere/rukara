namespace superhero.models;

public class User
{
    public int id { get; set; }
    public string username { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public byte[] passwordHash { get; set; } 
    public byte[] passwordSalt { get; set; }
    public bool isVerified { get; set; } = false;
}
