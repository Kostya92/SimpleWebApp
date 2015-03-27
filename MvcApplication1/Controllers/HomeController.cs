using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entities;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private AppContext _appContext = new AppContext("DefaultConnection");

        public ActionResult Index()
        {
            var profiles = _appContext.Profiles.ToList()
                    .Select(x =>
                            new ProfileModel
                            {
                                Id = x.Id,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email,
                                Phone = x.Phone
                            });

            return View(profiles);
        }

        [HttpGet]
        public ActionResult AddProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProfile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = new Profile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone
                };

                _appContext.Profiles.Add(profile);
                _appContext.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var profile = _appContext.Profiles.Find(id);
            if (profile == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ProfileModel
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Phone = profile.Phone
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(ProfileModel model)
        {
            var profile = _appContext.Profiles.Find(model.Id);
            if (profile == null)
            {
                ModelState.AddModelError("", "Profile not found");
            }

            if (ModelState.IsValid)
            {
                profile.FirstName = model.FirstName;
                profile.LastName = model.LastName;
                profile.Email = model.Email;
                profile.Phone = model.Phone;

                _appContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var profile = _appContext.Profiles.Find(id);
            if (profile == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Id = profile.Id;
            ViewBag.Message = string.Format("Are you sure you want to delete profile #{0} {1} {2}", profile.Id,
                profile.FirstName, profile.LastName);

            return View();
        }

        [HttpPost]
        public ActionResult DeleteSubmit(int id)
        {
            var profile = _appContext.Profiles.Find(id);
            if (profile == null)
            {
                return RedirectToAction("Index");
            }

            _appContext.Profiles.Remove(profile);
            _appContext.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
