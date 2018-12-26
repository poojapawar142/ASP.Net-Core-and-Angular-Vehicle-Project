
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebApplicationWithCore.Controllers
{
public class HomeController : Controller
    {
       public IActionResult Index()
       {
           //throw new Exception();
           
           return View();
       } 
       public IActionResult Error()
       {
           return View();
       }
    }
}