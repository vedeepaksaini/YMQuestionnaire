using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class TransactionViewModel
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public bool Deactive { get; set; }
        public string ControlValue { get; set; }
    }
}