using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.JsonPatch;

namespace Rocket_Elevator_RESTApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Elevators")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly InformationContext _context;

        public ElevatorsController(InformationContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
           
 //////This return all commercial elevators where date of last inspection is over a year
            DateTime current =  DateTime.Now.AddMonths(-12);

            var queryElevators = from elev in _context.elevators
                                 where elev.type_building == "Commercial" || elev.date_last_inspection < current
                                 select elev;

            var distinctElevators = (from elev in queryElevators
                                    select elev).Distinct();


            return await distinctElevators.ToListAsync();
        }

        
        [HttpPatch("{id}")]
        public async Task<ActionResult<Elevator>> Patch(int id, [FromBody]JsonPatchDocument<Elevator> info)
        {
            
            var elevator = await _context.elevators.FindAsync(id);

            info.ApplyTo(elevator);
            await _context.SaveChangesAsync();

            return elevator;
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetelevatorsStatus()
        {
            return  await _context.elevators
                    .Where(Elevator => Elevator.status != "Online" ).ToListAsync();
            
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(int id, Elevator elevator)
        {
            if (id != elevator.id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Elevators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevator", new { id = elevator.id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Elevator>> DeleteElevator(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return elevator;
        }

        private bool ElevatorExists(int id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}
