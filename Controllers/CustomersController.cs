using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController :Controller
    {
        private ApplicationDbContext _context;
        public CustomersController() {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing) {
           _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index() => View(_context.Customers.Include(c => c.MembershipType).ToList());

        public ActionResult New() {

            return View(new NewCustomerViewModel {
                membershipTypes = _context.membershipTypes.ToList()
            });
        }

        [HttpPost]
        public ActionResult Create(Customer customer) {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }
 
        public ActionResult Details(int? id) {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == (id ?? -1));
            if(customer == null)
                return HttpNotFound();
            return View(customer);
        }

    }
}