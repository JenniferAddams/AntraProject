using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    //Any mvc controller can become mvc controller if they are in this class.
    //        //Any mvc controller can become mvc controller if they are in this class.
    //http://localhost:2323/home/index
    //Routing: a PATTERN MATCHING TECHNIQUE 
    //homecontroler
    //index --Action method
    //Routing: a PATTERN MATCHING TECHNIQUE 
    //homecontroler
    //index --Action method
    public class HomeController : Controller
    {
        //Create a action method
        //public String Index()
        //{
        //    return "ABC";
        //}
        public IActionResult Index()
        {
            // return a instance of a class that implements that interface 1
            //By default MVC will look for same view name as action method name
            //it will look inside Views Folder-->home-->index.cshtml

            //Steps:
            //    1. Program.cs
            //    2. Main Method:invoke all; host method: inside this method all things executed.
            //    3. 
            //    4.

            return View();

        }
        public interface xyx
        {
            int Add(int x, int y);
        }
        public interface MyClass:xyx
        {
            int Add(int x, int y);
        }
    }
}
