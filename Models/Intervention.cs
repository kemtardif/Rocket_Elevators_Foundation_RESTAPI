using Microsoft.EntityFrameworkCore;
using System;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Intervention
    {


        public long id { get; }
        public DateTime? startDateIntervention { get; set; }
        public DateTime? endDateIntervention { get; set; }
        public string result { get; set; }
        public string report { get; set; }
        public string status { get; set; }

        


    }
}