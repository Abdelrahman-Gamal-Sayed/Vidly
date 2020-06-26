using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController :Controller
    {
        // GET: Customers
        public ActionResult Index() {

            return View(GetCustomers());
        }


        public ActionResult Details(int? id) {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == (id ?? -1));
            if(customer == null)
                return HttpNotFound();
            return View(customer);
        }
        IEnumerable<Customer> GetCustomers() {
            return new List<Customer> {
                new Customer{ Id=1,Name="Abdelrahman"},
                new Customer{ Id=2,Name="Gamal"},
                new Customer{ Id=3,Name="Sayed"},
                new Customer{ Id=4,Name="Mohamed"}

            };
        }
    }
}