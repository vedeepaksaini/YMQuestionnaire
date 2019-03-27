using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class ProcessTemplateStepsViewModel
    {
        public int ID { get; set; }
        public int ProcessID { get; set; }
        public int? ProcessOrder { get; set; }
        public string StepDescription { get; set; }
        public bool Deactive { get; set; }
    }
}