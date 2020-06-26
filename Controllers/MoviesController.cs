using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController :Controller
    {
        // GET: Movies
        public ActionResult Random() {
            var movie = new Movie() { Name = "shrek!", Id = 1 };
            var customers = new List<Customer> {
                new Customer{Id=1,Name="Customer 1"},
                new Customer{Id=2,Name="Customer 2"}

            };

            var viewModel = new RondomMovieViewModel {
                Customers = customers,
                Movie = movie
            };
            // ViewData["movies"] = movie;
            //  ViewBag.Movie = movie;
            return View(viewModel);
        }



        // GET: Customers
        public ActionResult Index() {

            return View(GetMovies());
        }


        public ActionResult Details(int? id) {
            var movies = GetMovies().SingleOrDefault(c => c.Id == ( id ?? -1 ));
            if(movies == null)
                return HttpNotFound();
            return View(movies);
        }
        IEnumerable<Movie> GetMovies() {
            return new List<Movie> {
                new Movie() { Name = "shrek! 1", Id = 1 },
               new Movie() { Name = "shrek! 2", Id = 2 },
                new Movie() { Name = "shrek! 3", Id = 3 },
                 new Movie() { Name = "shrek! 4", Id = 4 },
                  new Movie() { Name = "shrek! 5", Id = 5 }

            };
        }

    }
}