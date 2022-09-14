using Microsoft.AspNetCore.Mvc;
using superhero.models;

namespace superhero.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperheroController : ControllerBase
{
    private static List<Superhero> _heroes = new List<Superhero>
    {
        new Superhero
        {
            Id = 1,
            Name = "Batman",
            firstName = "clark",
            lastName = "Kent",
            Alias = "shuster",
                
        }
    };

    private  readonly  DataContext _context;
    public SuperheroController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Superhero>>> Get()
    {
        return Ok(await _context.Heroes.ToListAsync());
    } 
    
    [HttpPost]
    public async Task<ActionResult<List<Superhero>>> addHero(Superhero superhero)
    {
        _context.Heroes.Add(superhero);
        await _context.SaveChangesAsync();
        return Ok(await _context.Heroes.ToListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Superhero>> getOne(int id)
    {
        var hero = await _context.Heroes.FindAsync(id);
        if(hero== null)
        {
            return NotFound();
        }
        return Ok(hero);
    }

    [HttpPatch]
    public async Task<ActionResult<Superhero>> update(Superhero superhero)
    {
        Console.Write(superhero);
        var hero = await _context.Heroes.FindAsync(superhero.Id);
        if (hero==null)
        {
            return NotFound("solo not found");
        }

        hero.Name = superhero.Name;
        hero.firstName = superhero.firstName;
        hero.lastName = superhero.lastName;
        hero.Alias = superhero.Alias;
        await _context.SaveChangesAsync();
        return Ok(hero);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> remove(int id)
    {
        var hero = await _context.Heroes.FindAsync(id);
        if (hero == null)
        {
            return NotFound();
        }

        _context.Heroes.Remove(hero);
         var deleted = await _context.SaveChangesAsync();
        if (deleted==0)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
