using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// 1.  Purchase a Movie, HttpPost, store that info in the Purchase table
// http:localhost:12112/User/Purchase -- HttpPost
//First check if user already bought this movie.
//Buy button in the movie buttons page will call this method, if a user already bought this movie, then replace buy button with watch movie button.

//2. Get all the Movies Purchased by user, loged in User, take userid from HttpContext and get all the movies
// and give them to Movie Card partial view
// http:localhost:12112/User/Purchases -- HttpGet

// 3. Create a Review for a Movie for Loged In user , take userid from HttpContext and save info in Review Table
// http:localhost:12112/User/review -- HttpPost
//review button will open a pop up and ask user to enter a small review in text area and have enter movie rating from 1 to 10/

// 4. Get all the Reviews done my loged in User,
// http:localhost:12112/User/reviews -- HttpGet

// 5. Add a Favorite Movie for Loged In User
// http:localhost:12112/User/Favorite -- HttpPost
//add another button as favourite.
//Font Awesome library and use buttons from there.
//https://fontawesome.com/icons?d=gallery&q=favo

// 6.Check if a particular Movie has been added as Favorite by logedin user
// http:localhost:12112/User/{123}/movie/{12}/favorite  HttpGet

// 7. Remove favorite
// http:localhost:12112/User/Favorite -- Httpdelete

namespace MovieStore.MVC.Controllers
{
    public class UserController : Controller
    {
        //1. Purchase a moviE,HttpP
        public IActionResult Index()
        {
            return View();
        }
    }
}
