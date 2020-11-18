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
    [Route("api/Batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly InformationContext _context;

        public BatteriesController(InformationContext context)
        {
            _context = context;
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> Getbatteries()
        {
            return await _context.batteries
            .ToListAsync();
        }
         ////PATCH METHOD, where you change the status 
        
        [HttpPatch("{id}")]
        public async Task<ActionResult<Battery>> Patch(int id, [FromBody]JsonPatchDocument<Battery> info)
        {
            
            var battery = await _context.batteries.FindAsync(id);

            info.ApplyTo(battery);
            await _context.SaveChangesAsync();

            return battery;
        }

        // GET: api/Batteries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(int id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        // PUT: api/Batteries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBattery(int id, Battery battery)
        {
            if (id != battery.id)
            {
                return BadRequest();
            }

            _context.Entry(battery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Battery>> PostBattery(Battery battery)
        {
            _context.batteries.Add(battery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBattery", new { id = battery.id }, battery);
        }

        // DELETE: api/Batteries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Battery>> DeleteBattery(int id)
        {
            var battery = await _context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }

            _context.batteries.Remove(battery);
            await _context.SaveChangesAsync();

            return battery;
        }

        private bool BatteryExists(int id)
        {
            return _context.batteries.Any(e => e.id == id);
        }



    }
}
