using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class UserInterest
    {
        public UserInterest(int interestid, int userid)
        {
            InterestId = interestid;
            UserId = userid;
        }

        public int Id { get; set; }
        public int InterestId { get; set; }
        public int UserId { get; set; }
    }
}
