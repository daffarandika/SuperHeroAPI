using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }
        private static List<SuperHero> superHeroes = new List<SuperHero> {
            new SuperHero
            {
                Id = 1, Name = "Batman", FirstName = "Bruce", LastName = "Wayne", Place = "Gotham"
            },
            new SuperHero
            {
                Id = 2, Name = "Superman", FirstName = "Clark", LastName = "Kent", Place = "Metropolis"
            }
        };
        private readonly DataContext context;

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("heronya gak ketemu");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
            context.Add(superHero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("heronya ga ketemu");
            }
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteAHero(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("heronya gak ketemu");
            }
            context.SuperHeroes.Remove(hero);
            return Ok(await context.SuperHeroes.ToListAsync());
        }
    }
}
