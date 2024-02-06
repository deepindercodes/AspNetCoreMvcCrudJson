using AspNetCoreMvcCrudJson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreMvcCrudJson.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        string jsonFilePath = "";

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            jsonFilePath = _env.ContentRootPath.ToString() + ("/wwwroot/db/data.json");
        }

        public IActionResult Index()
        {
            jsonData objarticles = new jsonData();
            return View(objarticles.GetArticles(jsonFilePath));
        }

        public IActionResult AddNewArticle()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddNewArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                jsonData objarticles = new jsonData();

                ViewBag.ArticleAdded = "Y";

                objarticles.AddArticle(article, jsonFilePath);
            }

            return PartialView(article);
        }

        public IActionResult EditArticle(int id)
        {
            jsonData objarticles = new jsonData();
            return PartialView(objarticles.GetArticles(jsonFilePath).Where(u => u.id == id.ToString()).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                jsonData objarticles = new jsonData();
                ViewBag.ArticleEdited = "Y";

                objarticles.EditArticle(article,jsonFilePath);
            }

            return PartialView(article);
        }

        public IActionResult DelArticle(int id)
        {
            jsonData objarticles = new jsonData();
            objarticles.DeleteArticle(id.ToString(),jsonFilePath);

            return RedirectPermanent("/");
        }

        public IActionResult ViewArticle(int id)
        {
            jsonData objarticles = new jsonData();
            return View(objarticles.GetArticles(jsonFilePath).Where(u => u.id == id.ToString()).FirstOrDefault());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
