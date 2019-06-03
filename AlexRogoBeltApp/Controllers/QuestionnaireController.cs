using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AlexRogoBeltApp.Services;
using AlexRogoBeltApp.ViewModel;
using Newtonsoft.Json;

namespace AlexRogoBeltApp.Controllers
{
    public class QuestionnaireController : Controller
    {

        private readonly Service _service = new Service();

        public ActionResult GetStarted()
        {
            int MemberId = _service.IsMemberExist(Convert.ToInt32(Request.QueryString["MemberId"]));
            if (MemberId > 0)
            {
                HttpContext.Session["MemberId"] = MemberId;
                return View();
            }
            else
            {
                HttpContext.Session["MemberId"] = null;
                TempData["ErrorMsg"] = "You are not authenticated Member.";
             return  RedirectToAction("Dashboard");
            }
           
        }

        public ActionResult Dashboard()
        {
            
                return View();
        }
        public ActionResult Questions()
        {
            try
            {
                if (HttpContext.Session["MemberId"]!=null)
                {
                    if (Request.QueryString["QuestionOrder"] == null)
                    {
                        if (TempData["LevelId"] == null || TempData["OrderId"] == null)
                            TempData["LevelId"] = TempData["OrderId"] = 0;
                    }
                    else
                    {
                        TempData["OrderId"] = Convert.ToInt32(TempData["OrderId"]) - 1;
                    }

                    TempData.Keep("OrderId");
                    TempData.Keep("LevelId");

                    return View(_service.GetQuestions(Convert.ToInt32(TempData["LevelId"]), Convert.ToInt32(TempData["OrderId"])));
                }
                else
                {
                    TempData["ErrorMsg"] = "You are not authenticated Member.";
                    return RedirectToAction("Dashboard");
                }
            }
            catch( Exception ex)
            {
                TempData["ErrorMsg"] = "We are facing some issue at this time.";
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public ActionResult Questions(QuestionViewModel model, FormCollection frm)
        {
            try {
                if (HttpContext.Session["MemberId"] != null)
                {
                if (Convert.ToString(TempData["Slide"]) == "slide4")
                {
                    TempData.Remove("Slide");
                    TempData["LevelId"] = model.LevelID;
                    TempData["OrderId"] = model.QuestionOrder + 1;
                    return RedirectToAction("Questions");
                }
                if (Convert.ToString(TempData["Slide"]) == "empty")
                {
                    TempData.Remove("Slide");
                    TempData["LevelId"] = model.LevelID;
                    TempData["OrderId"] = model.QuestionOrder;
                    return RedirectToAction("Questions");
                }

                // For radio button 
                var ansId = frm["grp"];
                if (ansId != null)
                {
                    var answer = model.Answers.FirstOrDefault(x => x.ID.ToString() == ansId);
                    foreach (var item in model.Answers)
                    {
                        item.IsSelected = false;
                    }
                    answer.IsSelected = true;
                }

                var selectedAnswers = model.Answers.Where(x => x.IsSelected).ToList();
                selectedAnswers.AddRange(model.Answers.Where(x => !string.IsNullOrEmpty(x.ControlValue)));

                if (selectedAnswers.Count() == 0)
                {
                    TempData["OrderId"] = model.QuestionOrder;
                    TempData["LevelId"] = model.LevelID;
                    return RedirectToAction("Questions");
                }

                List<TransactionViewModel> transactions = selectedAnswers.Distinct().Select(x => new TransactionViewModel
                {
                    AnswerID = x.ID,
                    MemberID =Convert.ToInt32(HttpContext.Session["MemberId"]),
                    Deactive = x.Deactive,
                    QuestionID = x.QuestionID,
                    ControlValue = x.ControlValue
                }).ToList();

                _service.SetTransactions(transactions);

                TempData["LevelId"] = model.LevelID;
                TempData["OrderId"] = model.QuestionOrder + 1;

                if (TempData["OrderId"].ToString() == "14")
                    return PartialView("~/Views/YellowBelt/slide14.cshtml");
                else
                    return RedirectToAction("Questions");
            }
            else
            {
                TempData["ErrorMsg"] = "You are not authenticated Member.";
                return RedirectToAction("Dashboard");
            }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "We are facing some issue at this time.";
                return RedirectToAction("Dashboard");
            }
        }

        [WebMethod]
        public JsonResult GetAllTemplates()
        {
            //var processes = _service.GetAllTemplates();
            //ViewData["Processes"] = processes;
            //ViewBag.Processes = processes;
            return Json(_service.GetAllTemplates());
        }

        [WebMethod]
        public JsonResult GetTemplate(int id)
        {
            var data = _service.GetTemplate(id);
            return Json(data);
        }

        [WebMethod]
        public ActionResult SubmitTemplate(string data)
        {
            if (data.Replace("[]", "").Length == 0 || string.IsNullOrEmpty(data))
            {
                TempData["OrderId"] = 4;
                TempData["LevelId"] = 1;
                TempData["Slide"] = "empty";
                return RedirectToAction("Questions");
            }

            var steps = JsonConvert.DeserializeObject<string[]>(data);

            List<TransactionViewModel> transactions = steps.Select(x => new TransactionViewModel
            {
                AnswerID = 21,
                MemberID = Convert.ToInt32(HttpContext.Session["MemberId"]),
                Deactive = false,
                QuestionID = 8,
                ControlValue = x
            }).ToList();

            _service.SetTransactions(transactions);

            TempData["OrderId"] = 4 + 1;
            TempData["LevelId"] = 1;
            TempData["Slide"] = "slide4";

            return RedirectToAction("Questions");
        }

        [WebMethod]
        public JsonResult GetCalculations()
        {
            return Json(_service.GetCalculation());
        }

        [WebMethod]
       // [HttpPost]
        public ActionResult SubmitMarketSteps(string data)
        {
            //string[] data="";
            //if (data.Replace("[]", "").Length == 0 || string.IsNullOrEmpty(data))
            //{
            //    TempData["OrderId"] = 4;
            //    TempData["LevelId"] = 1;
            //    TempData["Slide"] = "empty";
            //    return RedirectToAction("Questions");
            //}

            //var steps = JsonConvert.DeserializeObject<string[]>(data);

            //List<TransactionViewModel> transactions = steps.Select(x => new TransactionViewModel
            //{
            //    AnswerID = 21,
            //    MemberID = 1,
            //    Deactive = false,
            //    QuestionID = 8,
            //    ControlValue = x
            //}).ToList();

            //_service.SetTransactions(transactions);

            //TempData["OrderId"] = 4 + 1;
            //TempData["LevelId"] = 1;
            //TempData["Slide"] = "slide4";

            return RedirectToAction("Questions");
        }
    }
}