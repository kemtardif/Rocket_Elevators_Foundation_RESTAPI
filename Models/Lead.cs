
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Rocket_Elevator_RESTApi.Models
{
public class Lead
    {
        public int id { get; set; }
        public int? customer_id { get; set; }
        public string full_name { get; set; }
        public string company_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public DateTime create_at { get; set; }
        

    }
}