using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EdmxDotNetCoreSample;

namespace EdmxAspNetCoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Entities _entities;

        public PeopleController(Entities entities)
        {
            _entities = entities;
        }

        [HttpGet("")]
        public async Task<IEnumerable<PersonDto>> Get(CancellationToken cancellationToken = default)
        {
            return await _entities.People
                .Select(p => new PersonDto
                {
                    Name = p.Name
                })
                .ToListAsync(cancellationToken);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(PersonDto dto, CancellationToken cancellationToken = default)
        {
            _entities.People.Add(new Person
            {
                Name = dto.Name
            });
            
            await _entities.SaveChangesAsync(cancellationToken);

            return StatusCode((int)HttpStatusCode.Created);
        }
    }

    public class PersonDto
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
    }
}
