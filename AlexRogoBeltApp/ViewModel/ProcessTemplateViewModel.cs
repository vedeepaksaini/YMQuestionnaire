using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class ProcessTemplateViewModel
    {
        public int ID { get; set; }
        public string ProcessTemplateName { get; set; }
        public int Steps { get; set; }
        public bool Deactive { get; set; }
        public List<ProcessTemplateStepsViewModel> ProcessTemplateSteps { get; set; }
    }
}