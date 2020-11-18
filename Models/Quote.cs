using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Quote 
    {
        public int id { get; set; }
        public string company_name { get; set; }
        public string building_type { get; set; }
        public string game { get; set; }
        public int total_price { get; set; }
        public int elevator_needed { get; set; }
        public DateTime create_at { get; set; }
    
    }
}