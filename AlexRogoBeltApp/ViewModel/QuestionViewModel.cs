using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class QuestionViewModel
    {
        public int ID { get; set; }
        public string Introduction { get; set; }
        public string Question { get; set; }
        public bool Deactive { get; set; }
        public int QuestionOrder { get; set; }
        public int LevelID { get; set; }
        public int SelectedValue { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
    }
}