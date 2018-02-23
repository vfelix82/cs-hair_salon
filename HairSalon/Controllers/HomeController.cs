using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
        List<Stylist> newStylist = Stylist.GetAll();
        return View(newStylist);
        }
        [HttpPost("/SaveClients")]
        public ActionResult AddItem()
        {
            string name = Request.Form["clientsname"];
            Stylist newStylist = new Stylist(name);
            newStylist.Save();
            return RedirectToAction("Index");
        }
    }
}
