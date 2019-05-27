using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexRogoBeltApp.Entities;
using AlexRogoBeltApp.ViewModel;

namespace AlexRogoBeltApp.Services
{
    public class Service
    {
        private readonly TOCICOEntities db = new TOCICOEntities();

        // Get all questions of yellow belt
        public QuestionViewModel GetQuestions(int? levelId, int? orderId)
        {
            QuestionMaster model = null;

            if (levelId == null || orderId == null || levelId == 0 || orderId == 0)
                levelId = orderId = 1;

            model = db.QuestionMasters.OrderBy(x => x.QuestionOrder).FirstOrDefault(x => x.LevelID == levelId && x.QuestionOrder == orderId && !x.Deactive);

            if (model == null)
                return null;

            return new QuestionViewModel
            {
                ID = model.ID,
                Introduction = model.Introduction,
                Question = model.Question,
                Deactive = model.Deactive,
                LevelID = model.LevelID,
                QuestionOrder = model.QuestionOrder,
                ViewName = model.ViewName,
                Answers = model.AnswerMasters.Where(p => !p.Deactive && p.QuestionID == model.ID).Select(y => new AnswerViewModel
                {
                    ID = y.ID,
                    Answers = y.Answers,
                    Deactive = y.Deactive,
                    QuestionID = y.QuestionID,
                    ControlID = y.ControlID,
                    ErrorAction = y.ErrorAction,
                    ErrorMSG = y.ErrorMSG,
                    ActionID = y.ActionID

                }).ToList(),
                Transactions = model.TransactionMasters.Where(q => !q.Deactive && q.QuestionID == model.ID).Select(z => new TransactionViewModel
                {
                    ID = z.ID,
                    AnswerID = z.AnswerID,
                    MemberID = z.MemberID,
                    QuestionID = z.QuestionID,
                    Deactive = z.Deactive,
                    ControlValue = z.ControlValue
                }
               ).ToList()

            };
        }

        public void SetTransactions(List<TransactionViewModel> models)
        {
            var questionId = models.FirstOrDefault().QuestionID;
            var existingTransactions = db.TransactionMasters.Where(x => x.MemberID == 1 && x.QuestionID == questionId);

            db.TransactionMasters.RemoveRange(existingTransactions);
            db.SaveChanges();

            db.TransactionMasters.AddRange(models.Select(x => new TransactionMaster
            {
                AnswerID = x.AnswerID,
                Deactive = x.Deactive,
                MemberID = x.MemberID,
                QuestionID = x.QuestionID,
                ControlValue = x.ControlValue
            }));
            db.SaveChanges();

            //if (models.Count != 0)
            //{
            //    var questionId = models.FirstOrDefault().QuestionID;
            //    var existingTransactions = db.TransactionMasters.Where(x => x.MemberID == 1 && x.QuestionID == questionId);

            //    //Update Existing records
            //    foreach (var transaction in models)
            //    {
            //        var record = existingTransactions.FirstOrDefault(x => x.AnswerID == transaction.AnswerID);
            //        if (record != null)
            //            record.ControlValue = transaction.ControlValue;
            //    }
            //    db.SaveChanges();

            //    //Remove existing record from models
            //    foreach (var transaction in existingTransactions)
            //    {
            //        var existing = models.FirstOrDefault(x => x.MemberID == transaction.MemberID && x.QuestionID == transaction.QuestionID && x.AnswerID == transaction.AnswerID);
            //        if (existing != null)
            //            models.Remove(existing);
            //    }

            //    //Insert fresh records
            //    db.TransactionMasters.AddRange(models.Select(x => new TransactionMaster
            //    {
            //        AnswerID = x.AnswerID,
            //        Deactive = x.Deactive,
            //        MemberID = x.MemberID,
            //        QuestionID = x.QuestionID,
            //        ControlValue = x.ControlValue
            //    }));
            //    db.SaveChanges();
            //}
        }

        public List<ProcessTemplateViewModel> GetAllTemplates()
        {
            var processes = db.ProcessTemplateMasters.Where(x => !x.Deactive);

            if (processes == null || processes.Count() == 0)
                return null;

            return processes.Select(y => new ProcessTemplateViewModel
            {
                ID = y.ID,
                ProcessTemplateName = y.ProcessTemplateName,
                Steps = y.Steps,
                Deactive = y.Deactive,
                ProcessTemplateSteps = y.ProcessTemplateSteps.OrderBy(x => x.ProcessOrder).Where(z => !z.Deactive && z.ProcessID == y.ID).Select(q => new ProcessTemplateStepsViewModel
                {
                    ID = q.ID,
                    ProcessID = q.ProcessID,
                    ProcessOrder = q.ProcessOrder,
                    StepDescription = q.StepDescription.Trim(),
                    Deactive = q.Deactive
                }).ToList()
            }).ToList();
        }

        public List<string> GetTemplate(int id)
        {
            return db.TransactionMasters.Where(x => x.QuestionID == id).OrderByDescending(x => x.ID).Select(x => x.ControlValue).ToList();

            //var process = db.ProcessTemplateMasters.FirstOrDefault(x => !x.Deactive && x.ID == 2);

            //if (process == null)
            //    return null;

            //return new ProcessTemplateViewModel
            //{
            //    ID = process.ID,
            //    ProcessTemplateName = process.ProcessTemplateName,
            //    Steps = process.Steps,
            //    Deactive = process.Deactive,
            //    ProcessTemplateSteps = process.ProcessTemplateSteps.OrderByDescending(x => x.ProcessOrder).Where(z => !z.Deactive && z.ProcessID == process.ID).Select(q => new ProcessTemplateStepsViewModel
            //    {
            //        ID = q.ID,
            //        ProcessID = q.ProcessID,
            //        ProcessOrder = q.ProcessOrder,
            //        StepDescription = q.StepDescription.Trim(),
            //        Deactive = q.Deactive
            //    }).ToList()
            //};
        }

        public List<CalculationsViewModel> GetCalculation()
        {
            var ans = new string[] { "Throughput", "Inventory", "Operating Expense" };

            var data = (from answers in db.AnswerMasters
                        join transaction in db.TransactionMasters on answers.ID equals transaction.AnswerID
                        where ans.Contains(answers.Answers)
                        select new CalculationsViewModel
                        {
                            Answers = answers.Answers,
                            Calculations = transaction.ControlValue
                        }).ToList();

            return data;
        }
    }
}