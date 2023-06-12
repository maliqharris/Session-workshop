using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Session.Models;
using Microsoft.AspNetCore.Http;

namespace Session.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }






    public IActionResult Index()
    {
      
        return View();
    }






    public IActionResult SetUser(string name)
    {
        HttpContext.Session.SetString("User", name);
        return RedirectToAction("Dashboard");
    }



    public IActionResult Dashboard()
    {
        string userName = HttpContext.Session.GetString("User");
       
        int? number = HttpContext.Session.GetInt32("Number");
        // NO way  this is right----------------------------------------------Only way to get 22 to display?
        if (number == null)
        {
            HttpContext.Session.SetInt32("Number", 22);
            number = 22;
        }
        // ------------------------------------------------------------------
        ViewData["UserName"] = userName;
        ViewData["Number"] = number;
        return View("CalcPage");
    }
    
    [HttpPost]
    public IActionResult CalculatePls(string operation)
    {
        int number = HttpContext.Session.GetInt32("Number") ?? 22;
        switch (operation)
        {
            case "+1":
                number += 1;
                break;
            case "-1":
                number -= 1;
                break;
            case "x2":
                number *= 2;
                break;
            case "random":
                Random random = new Random();
                number += random.Next(1, 11);
                break;
        }
        HttpContext.Session.SetInt32("Number", number);
        return RedirectToAction("Dashboard");
    }
    




    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
