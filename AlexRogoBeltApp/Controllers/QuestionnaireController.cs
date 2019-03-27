using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AlexRogoBeltApp.Services;
using AlexRogoBeltApp.ViewModel;

namespace AlexRogoBeltApp.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly Service _service = new Service();

        public ActionResult GetStarted()
        {
            return View();
        }

        public ActionResult StartLevel()
        {
            return View();
        }

        public ActionResult Questions()
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

        [HttpPost]
        public ActionResult Questions(QuestionViewModel model)
        {
            var selectedAnswers = model.Answers.Where(x => x.IsSelected || !string.IsNullOrEmpty(x.ControlValue));

            if (selectedAnswers.Count() == 0)
                return View(model);

            List<TransactionViewModel> transactions = selectedAnswers.Select(x => new TransactionViewModel
            {
                AnswerID = x.ID,
                MemberID = 1,
                Deactive = x.Deactive,
                QuestionID = x.QuestionID,
                ControlValue = x.ControlValue
            }).ToList();

            _service.SetTransactions(transactions);

            TempData["LevelId"] = model.LevelID;
            TempData["OrderId"] = model.QuestionOrder + 1;

            return RedirectToAction("Questions");
        }

        [WebMethod]
        public JsonResult GetAllTemplates()
        {
            //var processes = _service.GetAllTemplates();
            //ViewData["Processes"] = processes;
            //ViewBag.Processes = processes;
            return Json(_service.GetAllTemplates());
        }
    }
}