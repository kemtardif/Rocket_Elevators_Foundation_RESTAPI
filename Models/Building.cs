using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Building
    {

         ///////Basic attributes needed for the request, including collection of  related Batteries
        public int id { get; set;}
        public string adm_contact_name { get; set;}
        public string adm_contact_mail { get; set;}
        public string adm_contact_phone { get; set;}
        public string tect_contact_name { get; set;}
        public string tect_contact_email { get; set;}
        public string tect_contact_phone { get; set;}
        
        public virtual ICollection<Battery> Batteries { get; set;}



    }
}