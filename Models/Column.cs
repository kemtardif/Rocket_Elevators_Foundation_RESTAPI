using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Column 
    {

        /// ///////Basic attributes needed for the request, including related Battery_Id and all related Elevators
        public int id { get; set; }
        public string status { get; set; }
        public string type_building { get; set; }
        public int amount_floors_served { get; set; }
        
        public virtual int battery_id { get; set; }
        public  Battery Battery { get; set;}
        public virtual ICollection<Elevator> Elevators { get; }
        


    }
}