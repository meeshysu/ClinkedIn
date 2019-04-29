using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class UserServices
    {
        public UserServices(int serviceid, int userid)
        {
            ServiceId = serviceid;
            UserId = userid;
        }

        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
    }
}
