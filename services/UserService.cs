using Microsoft.OpenApi.Any;
using superhero.attributes;
using superhero.dtos;
using superhero.interfaces;
using superhero.models;


namespace superhero.services;


[ScopedService]
public class UserService
{
    private  readonly  DataContext _context;
    private readonly PasswordUtils _passwordUtils = new PasswordUtils();
    private readonly TokenUtil _tokenUtils = new TokenUtil();
    
    public UserService(DataContext context)
    {   
        _context = context;
    }

    public async Task<User> RegisterUser(UserDto request)
    {
        var exists = await this._context.Users.Where(item=> item.email == request.email).FirstOrDefaultAsync();
        if (exists != null)
        {
            throw new BadHttpRequestException("User already exist");
        }

        this._passwordUtils.HashPassword(request.password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User
        {
            email = request.email,
            passwordHash = passwordHash,
            passwordSalt = passwordSalt,
            username = request.username
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return await _context.Users.FindAsync(user.id);

    }
    
    public async Task<LoginResponse> LoginUser(LoginDto request)
    {
       var user = await _context.Users.Where(item => item.email == request.email).FirstOrDefaultAsync();
       if (user == null) 
       {
           throw new BadHttpRequestException("User does not exist");
       }
       Console.WriteLine(user.passwordHash);
       
         if (!_passwordUtils.VerifyPasswordHash(request.password,  user.passwordHash, user.passwordSalt))
         {
              throw new BadHttpRequestException("Invalid password or email");
         }
         
         
         var token = _tokenUtils.GetToken(user);
            return new LoginResponse
            {
                Token = token,
            };
    }
}
