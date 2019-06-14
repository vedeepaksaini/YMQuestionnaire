using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AlexRogoBeltApp.Entities;
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
            try
            {
                int id = Convert.ToInt32(TempData["MemberId"] == null ? Request.QueryString["MemberId"] : TempData["MemberId"]);
                var MemberDetails = _service.IsMemberExist(id);
                if (MemberDetails != null)
                //if (MemberId > 0)
                {
                    HttpContext.Session["MemberId"] = MemberDetails;// MemberDetails.MemberID;
                    return View();
                }
                else
                {
                    TempData["ErrorMsg"] = "You are not an authenticated Member.";
                    return RedirectToAction("Dashboard", new { MemberId = TempData["MemberId"] });
                }
            }
            catch (Exception e)
            {
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }


        public ActionResult Dashboard()
        {
            try

            {
                //TempData.Remove("ErrorMsg");
                var id = Request.QueryString["MemberId"];

                //PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                //// make collection editable
                //isreadonly.SetValue(this.Request.QueryString, false, null);
                //// remove
                //Request.QueryString.Remove("MemberId");

                var MemberDetails = _service.IsMemberExist(Convert.ToInt32(id));

                if (MemberDetails == null)
                    return UnauthorizedRequest(MemberDetails);

                if (!MemberDetails.IsYBpaymentCompleted)
                    TempData["ErrorMsg"] = "You have not purchased the Yellow Belt. Please goto YM E-Commerce and purchase this product.";
                else if (MemberDetails.IsYBStepsCompleted)
                    TempData["ErrorMsg"] = "You have completed the Yellow Belt.";
                else
                    TempData["MemberId"] = id;

                return View();
            }
            catch (Exception e)
            {
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }
        public ActionResult Questions()
        {
            try
            {
                var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

                if (MemberDetails == null)
                    return UnauthorizedRequest(MemberDetails);

                if (!MemberDetails.IsYBpaymentCompleted)
                {
                    TempData["ErrorMsg"] = "Pay for Yellow Belt first.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (MemberDetails.IsYBStepsCompleted)
                {
                    TempData["ErrorMsg"] = "You have completed Yellow Belt.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (Request.QueryString["QuestionOrder"] == null)
                {
                    if (TempData["LevelId"] == null || TempData["OrderId"] == null)
                    {
                        TempData["LevelId"] = TempData["OrderId"] = 0;
                    }
                    else if (Convert.ToString(TempData["slideno"]) == "slide14")
                    {
                        TempData["OrderId"] = Convert.ToInt32(TempData["OrderId"]) - 1;
                    }
                }
                else
                {
                    TempData["OrderId"] = Convert.ToInt32(TempData["OrderId"]) - 1;
                }

                TempData.Keep("OrderId");
                TempData.Keep("LevelId");
                return View(_service.GetQuestions(Convert.ToInt32(TempData["LevelId"]), Convert.ToInt32(TempData["OrderId"]), Convert.ToInt32(MemberDetails.MemberID)));

            }
            catch (Exception ex)
            {
                //TempData["ErrorMsg"] = "We are facing some issue at this time.";
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Questions(QuestionViewModel model, FormCollection frm)
        {
            try
            {
                var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

                if (MemberDetails == null)
                    return UnauthorizedRequest(MemberDetails);

                if (!MemberDetails.IsYBpaymentCompleted)
                {
                    TempData["ErrorMsg"] = "Pay for Yellow Belt first.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (MemberDetails.IsYBStepsCompleted)
                {
                    TempData["ErrorMsg"] = "You have completed Yellow Belt.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (Convert.ToString(TempData["Slide"]) == "slide4")
                {
                    TempData.Remove("Slide");
                    TempData["LevelId"] = model.LevelID;
                    TempData["OrderId"] = model.QuestionOrder + 1;
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
                }

                if (Convert.ToString(TempData["Slide"]) == "empty")
                {
                    TempData.Remove("Slide");
                    TempData["LevelId"] = model.LevelID;
                    TempData["OrderId"] = model.QuestionOrder;
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
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
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
                }

                List<TransactionViewModel> transactions = selectedAnswers.Distinct().Select(x => new TransactionViewModel
                {
                    AnswerID = x.ID,
                    MemberID = MemberDetails.MemberID,//Convert.ToInt32(HttpContext.Session["MemberId"]),
                    Deactive = x.Deactive,
                    QuestionID = x.QuestionID,
                    ControlValue = x.ControlValue
                }).ToList();

                _service.SetTransactions(transactions);

                TempData["LevelId"] = model.LevelID;
                TempData["OrderId"] = model.QuestionOrder + 1;

                if (TempData["OrderId"].ToString() == "14")
                {
                    //Mark yellow belt completed
                    _service.MarkYellowBeltCompleted(MemberDetails.MemberID);
                    HttpContext.Session["MemberId"] = null;
                    return PartialView("~/Views/YellowBelt/Congratulations.cshtml");
                }
                else
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
            }
            catch (Exception ex)
            {
                //TempData["ErrorMsg"] = "We are facing some issue at this time.";
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }

        [WebMethod]
        public JsonResult GetAllTemplates(int id)
        {
            //var processes = _service.GetAllTemplates();
            //ViewData["Processes"] = processes;
            //ViewBag.Processes = processes;
            return Json(_service.GetAllTemplates(id));
        }

        [WebMethod]
        public JsonResult GetTemplate(int id)
        {
            var data = _service.GetTemplate(id);
            //if (data[0] == null)
            //    data.Clear();
            return Json(data);
        }

        [WebMethod]
        public ActionResult SubmitTemplate(string data)
        {
            try
            {
                var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

                if (MemberDetails == null)
                    return UnauthorizedRequest(MemberDetails);

                if (data.Replace("[]", "").Length == 0 || string.IsNullOrEmpty(data))
                {
                    TempData["OrderId"] = 4;
                    TempData["LevelId"] = 1;
                    TempData["Slide"] = "empty";
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
                }

                var steps = JsonConvert.DeserializeObject<string[]>(data);
                List<TransactionViewModel> transactions = steps.Select(x => new TransactionViewModel
                {
                    AnswerID = 21,
                    MemberID = MemberDetails.MemberID,
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
            catch (Exception ex)
            {
                //TempData["ErrorMsg"] = "We are facing some issue at this time.";
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }

        private ActionResult UnauthorizedRequest(MemberMaster MemberDetails)
        {
            //TempData["ErrorMsg"] = "You are not an authenticated Member.";
            return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
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
            string[] MarketSteps = data.Split(',');
            var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

            if (MemberDetails == null)
                return UnauthorizedRequest(MemberDetails);

            if (data.Replace("[]", "").Length == 0 || string.IsNullOrEmpty(data))
            {
                TempData["OrderId"] = 13;
                TempData["LevelId"] = 1;
                TempData["Slide"] = "empty";
                return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
            }

            // var steps = JsonConvert.DeserializeObject<string[]>(data);
            List<TransactionViewModel> transactions = MarketSteps.Select(x => new TransactionViewModel
            {
                AnswerID = 38,
                MemberID = MemberDetails.MemberID,
                Deactive = false,
                QuestionID = 20,
                ControlValue = x
            }).ToList();

            _service.SetTransactions(transactions);

            TempData["OrderId"] = 14;
            TempData["LevelId"] = 1;
            TempData["Slideno"] = "slide14";

            return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
        }
    }
}