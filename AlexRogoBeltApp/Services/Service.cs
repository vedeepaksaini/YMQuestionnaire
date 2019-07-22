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
        public QuestionViewModel GetQuestions(int? levelId, int? orderId, int? MemberId)
        {
            QuestionMaster model = null;

            if (levelId == null || orderId == null || levelId == 0 || orderId == 0)
                levelId = orderId = 1;

            model = db.QuestionMasters.OrderBy(x => x.QuestionOrder).FirstOrDefault(x => x.LevelID == levelId && x.QuestionOrder == orderId && !x.Deactive);

            if (model == null)
                return null;

            var data = new QuestionViewModel
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
                Transactions = model.TransactionMasters.Where(q => !q.Deactive && q.QuestionID == model.ID && q.MemberID == MemberId).Select(z => new TransactionViewModel
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

            if (data != null && orderId == 2)
            {
                var member = db.MemberMasters.FirstOrDefault(x => x.MemberID == MemberId);

                if (member != null)
                {
                    data.Member = new MemberViewModel
                    {
                        MemberID = member.MemberID,
                        Email = member.Email,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Company = member.Company
                    };
                }

            }
            else
            {

            }
            data.SetpsCompleted = db.TransactionMasters.Where(x => x.MemberID == MemberId).Select(y => new { y.QuestionID }).Distinct().Count();
            data.TotalSetps = db.QuestionMasters.Select(x => new { x.ID }).Distinct().Count();
            return data;
        }

        public void SetTransactions(List<TransactionViewModel> models)
        {


            var questionId = models.FirstOrDefault().QuestionID;
            //  var tet = db.TransactionMasters.Select(x => x.MemberID == Convert.ToInt32(models[0].MemberID) && x.QuestionID == questionId).ToList();
            int MemberID = Convert.ToInt32(models[0].MemberID);
            var existingTransactions = db.TransactionMasters.Where(x => x.MemberID == MemberID && x.QuestionID == questionId);
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

           
        }


        public MemberMaster IsMemberExist(int MemberId)
        {
            var MemberCredentials = db.MemberMasters.Where(x => x.MemberID == MemberId).FirstOrDefault();

            // return MemberCredentials;
            if (MemberCredentials != null)
            {
                return MemberCredentials;
            }
            return null;
        }

        public ProductViewModel GetMemberIdExist(int MemberId)
        {
            var member = db.MemberMasters.FirstOrDefault(x => x.MemberID == MemberId);

            if (member != null)
            {
                return new ProductViewModel
                {
                    MemberID = member.MemberID,
                    Name = member.FirstName + member.LastName,
                    Company = member.Company,
                    Email = member.Email,
                  
                    IsBBpaymentCompleted = member.IsBBpaymentCompleted,
                    IsGBpaymentCompleted = member.IsGBpaymentCompleted,
                    IsYBpaymentCompleted = member.IsYBpaymentCompleted,
                    IsTOIpaymentCompleted = member.IsTOIpaymentCompleted,
                };
            }

            return null;
        }

        public List<ProcessTemplateViewModel> GetAllTemplates(int id)
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

        public List<string> GetTemplate(int id, int memberId)
        {
            return db.TransactionMasters.Where(x => x.QuestionID == id && x.MemberID == memberId).OrderByDescending(x => x.ID).Select(x => x.ControlValue).ToList();

       
        }

        public List<CalculationsViewModel> GetCalculation(int memberID)
        {
            var ans = new string[] { "Throughput", "Inventory", "Operating Expense" };

            var data = (from answers in db.AnswerMasters
                        join transaction in db.TransactionMasters on answers.ID equals transaction.AnswerID
                        where ans.Contains(answers.Answers) && transaction.MemberID == memberID
                        select new CalculationsViewModel
                        {
                            Answers = answers.Answers,
                            Calculations = transaction.ControlValue
                        }).ToList();

            return data;
        }
        public void MarkYellowBeltCompleted(int MemberId)
        {
            MemberMaster ObjMemberMaster = db.MemberMasters.First(c => c.MemberID == MemberId);
            ObjMemberMaster.IsYBStepsCompleted = true;
            db.SaveChanges();

        }
        public Steps CountSlideSteps(int memberId)
        {
            Steps data = new Steps();
            //data.SetpsCompleted = db.TransactionMasters.Select(x => new { x.QuestionID }).Distinct().Count();

            //changes By sadhana 11/july/19
            data.SetpsCompleted = db.TransactionMasters.Where(x => x.MemberID == memberId).Select(y => new { y.QuestionID }).Distinct().Count();
            data.TotalSetps = db.QuestionMasters.Select(x => new { x.ID }).Distinct().Count();
            return data;
        }

        public string GetHours(int member_Id, int question_Id, int answer_Id)
        {
            var Result = db.TransactionMasters.Where(x => x.QuestionID == question_Id && x.AnswerID == answer_Id && x.MemberID == member_Id).FirstOrDefault();
            return (Result.ControlValue);


        }

        public string ConfirmPayment(ProductViewModel model)
        {
            var obj = db.MemberMasters.Where(y => y.MemberID == model.MemberID).FirstOrDefault();

            if (obj != null)
            {
                obj.MemberID = model.MemberID;
                //AdminPassword = x.Mdetail.;
                obj.IsYBpaymentCompleted = model.IsYBpaymentCompleted;
                obj.IsGBpaymentCompleted = model.IsGBpaymentCompleted;
                obj.IsBBpaymentCompleted = model.IsBBpaymentCompleted;
                obj.IsTOIpaymentCompleted = model.IsTOIpaymentCompleted;
            }


            db.SaveChanges();

            if (obj == null)
            {
                return "Error Not Found";
            }

            return "Success";
        }

        public bool CheckAdminPassword(ProductViewModel model)
        {
            var result = db.MemberMasters.Where(y => y.MemberID == 1 && y.WebsiteID == 1 && y.MemberUserName == "Admin" && y.Email == model.AdminPassword).FirstOrDefault();
            if (result == null)
            {
                return false;
            }

            return true;

        }
    }
}