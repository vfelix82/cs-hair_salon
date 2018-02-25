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

        [HttpGet("/Clients")]
        public ActionResult Clients()
        {
            List<Client> newClient = Client.GetAll();
            return View(newClient);
        }

        [HttpPost("/SaveStylist")]
        public ActionResult AddStylist()
        {
            string name = Request.Form["stylistname"];
            Stylist newStylist = new Stylist(name);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/SaveClient")]
        public ActionResult AddClient()
        {
            string name = Request.Form["clientname"];
            string stylistname = Request.Form["stylistname"];
            Client newClient = new Client(name, stylistname);
            newClient.Save();
            return RedirectToAction("Clients");
        }

        [HttpGet("/Stylist/Delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();

            return RedirectToAction("Index");
        }

        [HttpGet("/Stylist/{id}")]
        public ActionResult Info(int id)
        {
            return View(Stylist.Find(id));
        }

    }
}
