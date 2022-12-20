using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult View(int id)
        {
            Debug.WriteLine("ID: " + id.ToString());
            Person? p = Personal_info.GetPerson(id);
            if (p != null) return View(p);

            ViewBag.error = "Not found";
            return View("ErrorAction");
        }

        public IActionResult All()
        {
            List<Person> res = Personal_info.GetAllPerson();
            
            return View(res);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Search(Person persone)
        {
            Person? res = Personal_info.searchPerson(persone);

            int redirectId;
            if (res == null) redirectId = -1;
            else redirectId = res.id;
            return RedirectToRoute(new {
                id = redirectId,
                controller = "Person",
                action = "View"
            });
        }
    }
}
