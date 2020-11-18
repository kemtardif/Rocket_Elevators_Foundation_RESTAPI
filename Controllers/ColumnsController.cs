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
    [Route("api/Columns")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly InformationContext _context;

        public ColumnsController(InformationContext context)
        {
            _context = context;
        }

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> Getcolumns()
        {
            return await _context.columns
            .Include(b => b.Battery)
            .ToListAsync();
        }

        
        [HttpPatch("{id}")]
        public async Task<ActionResult<Column>> Patch(int id, [FromBody]JsonPatchDocument<Column> info)
        {
            
            var column = await _context.columns.FindAsync(id);

            info.ApplyTo(column);
            await _context.SaveChangesAsync();

            return column;
        }

        // GET: api/Columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

        // PUT: api/Columns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumn(int id, Column column)
        {
            if (id != column.id)
            {
                return BadRequest();
            }

            _context.Entry(column).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnExists(id))
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

        // POST: api/Columns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Column>> PostColumn(Column column)
        {
            _context.columns.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumn", new { id = column.id }, column);
        }

        // DELETE: api/Columns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Column>> DeleteColumn(int id)
        {
            var column = await _context.columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.columns.Remove(column);
            await _context.SaveChangesAsync();

            return column;
        }

        private bool ColumnExists(int id)
        {
            return _context.columns.Any(e => e.id == id);
        }

    }
}
