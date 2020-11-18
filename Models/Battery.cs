using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Rocket_Elevator_RESTApi.Models
{
    
    public partial class Battery
    {
            ///////Basic attributes needed for the request, including related Building_Id and all related Columns
        public int id { get; set; }
        public string status { get; set; }
        public DateTime date_last_inspection { get; set; }
        public string cert_ope { get; set; }


        public virtual int building_id { get; set;}
        public  Building Building { get; set;}

        public virtual ICollection<Column> Columns { get; set;}

    }
}