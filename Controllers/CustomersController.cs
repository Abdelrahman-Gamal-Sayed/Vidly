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

        public ActionResult Edit(int id) {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                return HttpNotFound();

            var viewModel = new NewCustomerViewModel {

                membershipTypes = _context.membershipTypes.ToList(),
                Customer = customer

            };
            return View("New", viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer) {

            if(!ModelState.IsValid) {

                var viewModel = new NewCustomerViewModel {

                    membershipTypes = _context.membershipTypes.ToList(),
                    Customer = customer

                };
                return View("New", viewModel);
            }
            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else {
                var cust = _context.Customers.Single(c => c.Id == customer.Id);

                cust.Name = customer.Name;
                cust.Birthdate = customer.Birthdate;
                cust.MembershipTypeId = customer.MembershipTypeId;
                cust.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
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