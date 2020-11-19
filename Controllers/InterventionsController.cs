using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;

namespace Rocket_Elevator_RESTApi.Controllers
{
    [Route("api/interventions")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly InformationContext _context;

        public InterventionsController(InformationContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

////////////////GET PENDING REQUESTS///////////////////

        [Route("/pendingRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> PendingRequests()
        {
           var queryIntervention = from intervention in _context.interventions
                                        where intervention.startDateIntervention == null 
                                        where intervention.status == "Pending"
                                        select intervention;

            var distinctInterventions = (from intervention in queryIntervention
                                            select intervention).Distinct();

           
            return  await distinctInterventions.ToListAsync();

            
        }
////////////////GET INPROGRESS REQUESTS///////////////////

        [Route("/inProgressRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> inProgressRequests()
        {
           var queryIntervention = from intervention in _context.interventions
                                        where intervention.startDateIntervention != null 
                                        where intervention.status == "InProgress"
                                        select intervention;

            var distinctInterventions = (from intervention in queryIntervention
                                            select intervention).Distinct();

           
            return  await distinctInterventions.ToListAsync();

            
        }

////////////////GET COMPLETED REQUESTS///////////////////

        [Route("/completedRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> completedRequests()
        {
           var queryIntervention = from intervention in _context.interventions
                                        where intervention.endDateIntervention != null 
                                        where intervention.status == "Completed"
                                        select intervention;

            var distinctInterventions = (from intervention in queryIntervention
                                            select intervention).Distinct();

           
            return  await distinctInterventions.ToListAsync();

            
        }


/////////////////UPDATE START DATE////////////////////////

        [HttpPut("/startIntervention/{id}")]
        public async Task<ActionResult<Intervention>> StartIntervention(long Id)
        {
            var toUpdate = _context.interventions.FirstOrDefault(u => u.id == Id);

            if (toUpdate.startDateIntervention == null && toUpdate.status == "Pending" ){

                toUpdate.startDateIntervention =  DateTime.Now;
                toUpdate.status = "InProgress";
            }else{

                return NotFound();
            }


            _context.interventions.Update(toUpdate);
            await  _context.SaveChangesAsync();

            return  toUpdate;
        }

//////////////END START DATE//////////////////

        [HttpPut("/endIntervention/{id}")]
        public async Task<ActionResult<Intervention>> endIntervention(long Id)
        {
            var toUpdate = _context.interventions.FirstOrDefault(u => u.id == Id);

            if (toUpdate.startDateIntervention != null &&  toUpdate.endDateIntervention == null){
                toUpdate.endDateIntervention =  DateTime.Now;
                toUpdate.status = "Completed";

            }else{

                return NotFound();
            }


            _context.interventions.Update(toUpdate);
            await  _context.SaveChangesAsync();


            return  toUpdate;
        }

        // POST: api/Interventions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.id }, intervention);
        }

        // DELETE: api/Interventions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Intervention>> DeleteIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return intervention;
        }



        private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.id == id);
        }
    }
}
