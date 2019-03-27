using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexRogoBeltApp.ViewModel
{
    public class Questionnaire
    {
        public List<QuestionViewModel> Questions { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
    }
}