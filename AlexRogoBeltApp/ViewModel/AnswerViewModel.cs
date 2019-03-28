using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class AnswerViewModel
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string Answers { get; set; }
        public bool Deactive { get; set; }
        public string ErrorMSG { get; set; }
        public string ErrorAction { get; set; }
        public int? ControlID { get; set; }
        public bool IsSelected { get; set; }
        public string ControlValue { get; set; }
        public int? ActionID { get; set; }
    }
}