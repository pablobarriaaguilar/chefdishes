using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsdishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chefsdishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;  
    public ChefController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        
        return View("Index");   
        
    }
[HttpGet]
[Route("Listchefs")]
public IActionResult Listchefs(){

    List<Chef> listchefs = _context.Chefs.Include(chef => chef.AllDishes).ToList();
    return View("ListChefs",listchefs);
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
    [Route("Chef/Create")]
        public IActionResult Create(Chef _chef){
            if(ModelState.IsValid){
        _context.Add(_chef);
        _context.SaveChanges();
        return RedirectToAction("Index");
            }else{
                return View("Index",_chef);
            }
      

    }
}
