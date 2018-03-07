using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {

        // Clients //
        [HttpGet("/ClientList")]
        public ActionResult ClientList()
        {
            return View(Client.GetAll());
        }

        [HttpGet("/Client/{stylistId}")]
        public ActionResult ClientsForm(int stylistId)
        {
            Stylist myStylist = Stylist.Find(stylistId);
            return View(myStylist);
        }

        [HttpPost("/ClientAdd")]
        public ActionResult ClientAdd()
        {
            string clientsName = Request.Form["clientsname"];
            int stylistId = Int32.Parse(Request.Form["stylistId"]);
            Client newClient = new Client(clientsName, stylistId);
            newClient.Save();
            return RedirectToAction("StylistDetails", new {id=stylistId});
        }

        [HttpPost("/ClientList/delete-all")]
        public ActionResult DeleteAllClient()
        {
            Client.DeleteAll();
            return View("ClientList");
        }

        [HttpPost("/Client/{id}/delete")]
        public ActionResult DeleteClient(int id)
        {
            Client myClient = Client.Find(id);
            myClient.Delete();
            return RedirectToAction("ClientList");
        }

        // Stylist //
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(Stylist.GetAll());
        }

        [HttpGet("/StylistDetails/{id}")]
        public ActionResult StylistDetails(int id)
        {
            Stylist myStylist = Stylist.Find(id);
            return View(myStylist);
        }

        [HttpPost("/StylistAdd")]
        public ActionResult StylistAdd()
        {
            string stylistName = Request.Form["stylistname"];
            Stylist newStylist = new Stylist(stylistName);
            newStylist.Save();
            return RedirectToAction("Index", newStylist);
        }

        [HttpPost("/delete-all")]
        public ActionResult DeleteAllStylist()
        {
            Stylist.DeleteAll();
            return View("Index");
        }

        [HttpPost("/Stylist/{id}/delete")]
        public ActionResult DeleteStylist(int id)
        {
            Stylist myStylist = Stylist.Find(id);
            myStylist.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/stylist/{stylistId}")]
        public ActionResult Update(int stylistId)
        {
            string name = Request.Form["editstylistname"];
            string specialties = Request.Form["stylistspecialties"];
            if(specialties != null)
            {
                String[] specialtyIds = specialties.Split(',');
                foreach(var specialtyId in specialtyIds)
                {
                    Stylist.Find(stylistId).AddSpecialty(Specialty.Find(Int32.Parse(specialtyId)));
                }
            }
            Stylist myStylist = Stylist.Find(stylistId);
            myStylist.Edit(name);
            return RedirectToAction("StylistDetails", new {id = stylistId});
        }

        // Specialties //

        [HttpGet ("/SpecialtyList")]
        public ActionResult SpecialtyList()
        {
            return View ("SpecialtyList", Specialty.GetAll());
        }

        [HttpGet ("/Specialty")]
        public ActionResult SpecialtyForm(int stylistId)
        {
            Stylist myStylist = Stylist.Find (stylistId);
            return View (myStylist);
        }

        [HttpPost ("/SpecialtyAdd")]
        public ActionResult SpecialtyAdd()
        {
            string name = Request.Form["specialtyname"];
            Specialty newSpecialty = new Specialty(name);
            newSpecialty.Save ();
            return RedirectToAction ("SpecialtyList");
        }

        [HttpGet ("/SpecialtyList/delete-all")]
        public ActionResult DeleteAllSpecialties()
        {
            Specialty.DeleteAll();
            return View("SpecialtyList");

        }
    }
}
