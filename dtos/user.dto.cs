namespace superhero.dtos;

public class UserDto
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}

public class LoginDto
{
    public string email { get; set; }
    public string password { get; set; }
}
