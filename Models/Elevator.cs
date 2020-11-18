using Microsoft.EntityFrameworkCore;
using System;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Elevator
    {

         ///////Basic attributes needed for the request, including related Column
        public int id { get; }
        public string status { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public string type_building { get; set; }
        public DateTime date_last_inspection { get; set; }

        public virtual int column_id { get; set; }
        public  Column Column { get; }

    }
}