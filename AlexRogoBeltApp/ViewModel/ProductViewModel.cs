using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        public int MemberID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public bool IsGBpaymentCompleted { get; set; }
        public bool IsYBpaymentCompleted { get; set; }
        public bool IsBBpaymentCompleted { get; set; }
        public bool IsTOIpaymentCompleted { get; set; }
        [Required]
        public string AdminPassword { get; set; }
        public string Error { get; set; }
    }
}