using AlexRogoBeltApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class MemberViewModel
    {
        public int MemberID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
       // public MemberMaster Memberdetail { get; set; }
        
    }

    
}