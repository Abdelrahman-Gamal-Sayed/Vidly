using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController :Controller
    {
        ApplicationDbContext context;
        public MoviesController() {
            context = new ApplicationDbContext();
        }

        public ActionResult New() {

            return View(new NewMovieViewModel { genres = context.Genres.ToList() });
        }
        [HttpPost]
        public ActionResult Create(Movie movie) {


            movie.DateAdded = DateTime.Now;

            context.Movies.Add(movie);
            context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        public ActionResult Edit(Movie movie) {


            var mo = context.Movies.SingleOrDefault(m => m.Id == movie.Id);
            if(mo == null)
                return HttpNotFound();

            mo.Name = movie.Name;
            mo.GenreId = movie.GenreId;
            mo.NumberInStock = movie.NumberInStock;
            mo.ReleaseDate = movie.ReleaseDate;

            context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id) {
            var movie = context.Movies.FirstOrDefault(a => a.Id == id);

            if(movie == null)
                return HttpNotFound();

            return View(new NewMovieViewModel { Movie = movie, genres = context.Genres.ToList() });
        }
        protected override void Dispose(bool disposing) {
            context.Dispose();
        }
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

            return View(context.Movies.Include(g=>g.Genre).ToList());
        }


        public ActionResult Details(int? id) {
            var movies = context.Movies.Include(g => g.Genre).SingleOrDefault(c => c.Id == ( id ?? -1 ));
            if(movies == null)
                return HttpNotFound();
            return View(movies);
        }
   

    }
}