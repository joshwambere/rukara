using Microsoft.EntityFrameworkCore;
using superhero.models;

namespace superhero.data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }
    
    public DbSet<Superhero> Heroes { get; set; }
    public DbSet<User> Users { get; set; }
}
