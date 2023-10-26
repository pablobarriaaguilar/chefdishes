using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsdishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chefsdishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;  
    public DishController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {

        List<Chef> AllDishes = _context.Chefs.ToList();
            ViewBag.AllChefs = _context.Chefs.ToList();
        
        return View("Form");   
        
    }

[HttpGet]
[Route("listdishes")]
     public IActionResult Listdishes(){
        List<Dish> listdishes = _context.Dishes.Include(c => c.Creator).ToList();
        return View("Listdishes",listdishes);
     }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

      [HttpPost]
    [Route("Dish/Create")]
        public IActionResult Create(Dish _dish){
            if(ModelState.IsValid){
        _context.Add(_dish);
        _context.SaveChanges();
        return RedirectToAction("Index");
            }else{
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View("Form",_dish);
            }
        

    }
}
