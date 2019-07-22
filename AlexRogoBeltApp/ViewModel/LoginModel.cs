using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        public string Password { get; set; }

        public string Error { get; set; }
       
    }
}