using lab12.Models;
using lab12.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace lab12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService _iservice;

        public HomeController(ILogger<HomeController> logger, IService iservice)
        {
            _logger = logger;
            _iservice = iservice;
        }
        private string parseData()
        {
            try
            {
                int firstNumber = int.Parse(Request.Form["firstNumber"]);
                int secondNumber = int.Parse(Request.Form["secondNumber"]);
                string opertaion = Request.Form["operation"];
                return _iservice.calc(firstNumber, secondNumber, opertaion);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manual()
        {
            if (Request.Method == "GET")
                return View("Form");
            else
            {
                ViewData["result"] = parseData();
                return View("Result");
            }
        }


        [HttpGet, ActionName("ManualSeparate")]
        public IActionResult ManualSeperateGet()
        {
            return View("Form");
        }

        [HttpPost, ActionName("ManualSeparate")]
        public IActionResult ManualSeperatePost()
        {
            ViewData["Result"] = parseData();
            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingSeperate()
        {
            return View("Form");
        }

        [HttpPost]
        public IActionResult ModelBindingSeperate(int firstNumber, int secondNumber, string operation)
        {
            ViewData["Result"] = _iservice.calc(firstNumber, secondNumber, operation);
            return View("Result");
        }


        [HttpGet]
        public IActionResult ModelBinding()
        {
            return View("Form");
        }

        [HttpPost]
        public IActionResult ModelBinding(EnterData expressionModel)
        {
            ViewData["Result"] = _iservice.calc(expressionModel);
            return View("Result");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}