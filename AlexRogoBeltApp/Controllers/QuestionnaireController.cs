using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        private readonly YMServices _ymservice = new YMServices();

        public ActionResult GetStarted()
        {
            try
            {

                bool isValid = int.TryParse(Request.QueryString["MemberId"], out int res);

                if (!isValid)
                    return UnauthorizedRequest();

                int id = Convert.ToInt32(TempData["MemberId"] == null ? res : TempData["MemberId"]);
                var MemberDetails = _service.IsMemberExist(id);
                if (MemberDetails != null)
              
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
           
                bool isValid = int.TryParse(Request.QueryString["MemberId"], out int res);

                if (!isValid)
                    return UnauthorizedRequest();


                var MemberDetails = _service.IsMemberExist(Convert.ToInt32(res));


                if (MemberDetails != null)
                {

                    if (!MemberDetails.IsYBpaymentCompleted)
                  
                        TempData["ErrorMsg"] = "You have not purchased the Yellow Belt. Please goto YM E-Commerce and purchase this product.";
                    else if (MemberDetails.IsYBStepsCompleted)
                        TempData["SuccessMsg"] = "You have completed the Yellow Belt.";
                    else
                        TempData["MemberId"] = res;

                    return View(_service.CountSlideSteps(res));
                }
                return UnauthorizedRequest();


            }
            catch (Exception e)
            {
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }



        //Method call on Get Start Button.
        public ActionResult Questions()
        {
            try
            {

                var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];
                if (MemberDetails == null)
                {
                    return SessionExpired();
                }

                if (!MemberDetails.IsYBpaymentCompleted)
                {
                    
                    TempData["ErrorMsg"] = "Pay for Yellow Belt first.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (MemberDetails.IsYBStepsCompleted)
                {
                    TempData["SuccessMsg"] = "You have completed Yellow Belt.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }



                if (Request.QueryString["QuestionOrder"] == null)
                {
                    if (TempData["LevelId"] == null || TempData["OrderId"] == null)
                    {
                        TempData["LevelId"] = TempData["OrderId"] = 0;
                    }
                    //****** 3 july 2019 Sadhana******** To complete step slide should be 23
                    else if (Convert.ToString(TempData["slideno"]) == "slide23")
                    {
                        TempData["OrderId"] = Convert.ToInt32(TempData["OrderId"]) - 1;
                    }
                }
                else
                {
                    if (Convert.ToInt32(TempData["OrderId"]) == Convert.ToInt32(Request.QueryString["QuestionOrder"]))
                        TempData["OrderId"] = Convert.ToInt32(TempData["OrderId"]) - 1;
                    else { }
                }

                TempData.Keep("OrderId");
                TempData.Keep("LevelId");
                return View(_service.GetQuestions(Convert.ToInt32(TempData["LevelId"]), Convert.ToInt32(TempData["OrderId"]), Convert.ToInt32(MemberDetails.MemberID)));

            }
            catch (Exception ex)
            {

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
                    return SessionExpired();

                if (!MemberDetails.IsYBpaymentCompleted)
                {
                   
                    TempData["ErrorMsg"] = "Pay for Yellow Belt first.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (MemberDetails.IsYBStepsCompleted)
                {
                    TempData["SuccessMsg"] = "You have completed Yellow Belt.";
                    return RedirectToAction("Dashboard", new { MemberId = MemberDetails.MemberID });
                }

                if (Convert.ToString(TempData["Slide"]) == "slide4")
                {
                    TempData.Remove("Slide");
                    TempData["LevelId"] = model.LevelID;
                    TempData["OrderId"] = model.QuestionOrder + 1;
                    return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
                }

                if (Convert.ToString(TempData["Slide"]) == "slide13")
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

                if (model.QuestionOrder != 23)
                {
                    List<TransactionViewModel> transactions = selectedAnswers.Distinct().Select(x => new TransactionViewModel
                    {
                        AnswerID = x.ID,
                        MemberID = MemberDetails.MemberID,//Convert.ToInt32(HttpContext.Session["MemberId"]),
                        Deactive = x.Deactive,
                        QuestionID = x.QuestionID,
                        ControlValue = x.ControlValue
                    }).ToList();

                    _service.SetTransactions(transactions);
                }

                TempData["LevelId"] = model.LevelID;
                TempData["OrderId"] = model.QuestionOrder + 1;

                if (TempData["OrderId"].ToString() == "23")
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
        public JsonResult GetHours()
        {
            var member_Id = ((MemberMaster)HttpContext.Session["MemberId"]).MemberID;

            var question_Id = 32;
            var answer_Id = 47;
            return Json(_service.GetHours(member_Id, question_Id, answer_Id));


        }

        [WebMethod]
        public JsonResult GetAllTemplates(int id)
        {
          
            return Json(_service.GetAllTemplates(id));
        }

        [WebMethod]
        public JsonResult GetTemplate(int id)
        {
            var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];
            var data = _service.GetTemplate(id, MemberDetails.MemberID);
          
            return Json(data);
        }

        [WebMethod]
        public ActionResult SubmitTemplate(string data)
        {
            try
            {
                var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

                if (MemberDetails == null)
                    return SessionExpired();

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

        private ActionResult UnauthorizedRequest()//MemberMaster MemberDetails
        {
            TempData["ErrorMsg"] = "You are not an authenticated Member.";
            return View("Dashboard");
        }
        private ActionResult SessionExpired()
        {
            var MemberDetails = _service.IsMemberExist(Convert.ToInt32(Request.QueryString["MemberId"]));
            if (MemberDetails==null)
            {
                return UnauthorizedRequest();
            }
            TempData["InfoMsg"] = "Session has been expired.";

            return View("Dashboard");
        }

        [WebMethod]
        public JsonResult GetCalculations()
        {
            var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];
            return Json(_service.GetCalculation(MemberDetails.MemberID));
        }

        [WebMethod]
        // [HttpPost]
        public ActionResult SubmitMarketSteps(string data)
        {
            string[] MarketSteps = data.Split(',');
            var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];

            if (MemberDetails == null)
                return SessionExpired();

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

            TempData["OrderId"] = 13 + 1;
            TempData["LevelId"] = 1;
            TempData["Slide"] = "slide13";

            //return RedirectToAction("Questions");
            return RedirectToAction("Questions", new { MemberId = MemberDetails.MemberID });
        }
        [HttpPost]
        public ActionResult LoadMemebrGUID( string id)
        {
            ProductViewModel model = new ProductViewModel();
            model.AdminPassword = id;
            if ((_service.CheckAdminPassword(model)) == true)

            {    
                //Getting all GUID from Server
                 model.Success = _ymservice.GetAllGuid();
                 return Json(model, JsonRequestBehavior.AllowGet);


            }
            else
            {
                model.Error = "Invalid Password";
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            var MemberDetails = (MemberMaster)HttpContext.Session["MemberId"];
            if (MemberDetails == null)
            {
                MemberDetails = _service.IsMemberExist(Convert.ToInt32(Request.QueryString["MemberId"]));
            }
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Dashboard", new { MemberId = MemberDetails == null ? Convert.ToInt32(Request.QueryString["MemberId"]) : MemberDetails.MemberID });
        }


        //Created By Sadhana 11 july 19

       //Method to set the session of user after confirmation to continue session
        public ActionResult Setsessiondata()
        {

            var memberId = Request.QueryString["MemberId"];

            var MemberDetails = _service.IsMemberExist(Convert.ToInt32(memberId));

            HttpContext.Session["MemberId"] = MemberDetails;// MemberDetails.MemberID;

            return RedirectToAction("Questions", new { MemberId = memberId });

        }



        //Created By Sadhana 17 july 19
        [HttpGet]
        public ActionResult PaymentUpdate()
      {
            return View();
        }

        //Created By Sadhana 16 july 19

            //Method to get the MemberDeatil of specific member requested from the PaymentUpdate Page.
        [HttpPost]
        public ActionResult PaymentUpdate(int MID)
        {
            ProductViewModel model = new ProductViewModel();
            model.MemberID = MID;

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var member = _service.GetMemberIdExist(Convert.ToInt32(model.MemberID));

                if (member == null)
                {
                    model.Error = "Member not found";
                    model.MemberID = 0;
                    return Json(model, JsonRequestBehavior.AllowGet);
                    //return View(model);
                }

                return Json(member, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return PartialView(@"~\Views\Shared\Error.cshtml");
            }
        }

        //Created By Sadhana 16 july 19

            //method to update data in member master table posted from PaymentUpdatePage
        [HttpPost]
        public ActionResult ConfirmPayment(ProductViewModel model)
        {
            if ((_service.CheckAdminPassword(model)) == true)
            {
                var result = _service.ConfirmPayment(model);
                if (result == "Success")
                {
                    model.Success = "Payment Updated successfully";
                    model.MemberID = 0;
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
            
                model.Error = "Invalid Password";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //method to login admin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel LoginModel)


        {
            var result = _service.CheckLoginCredential(LoginModel);
            if(result==true)
            {
                ViewBag.UserName = LoginModel.UserID;
                
                return View("PaymentUpdate");
            }
           
            ViewBag.Error = "invalid User Name or Password";
           
            ModelState.Clear();
            return View();
        }
    }
}